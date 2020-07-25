using System;
using System.IO;
using System.Security.Cryptography;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System.Text;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Crypto.Generators;
using YOY.Sys.Security.Security.Cryptography;

namespace YOY.Security.Cryptography
{
    public static class RSASecurity
    {
        #region PROPERTIES

        private const int RsaKeySize = 4096;
        private static string PEMPublicKey;
        private static string PEMPrivateKey;
        private static AsymmetricKeyParameter privateKey;
        private static AsymmetricKeyParameter publicKey;

        #endregion

        #region METHODS

        static void ReadPrivateKey(string privateKeyFileName)
        {
            AsymmetricCipherKeyPair keyPair;

            using (var reader = File.OpenText(privateKeyFileName))
                keyPair = (AsymmetricCipherKeyPair)new PemReader(reader).ReadObject();

            privateKey = keyPair.Private;
        }

        static void ReadPublicKey(string publicKeyFileName)
        {
            var fileStream = File.OpenText(publicKeyFileName);
            var pemReader = new PemReader(fileStream);
            var KeyParameter = (AsymmetricKeyParameter)pemReader.ReadObject();
            publicKey = KeyParameter;
        }

        public static void RestoresKeys(string publicKeyPath, string privateKeyPath)
        {
            try
            {

                ReadPrivateKey(privateKeyPath);
                ReadPublicKey(publicKeyPath);
                
            }
            catch(Exception)
            {
                //FAILED
            }

        }
        
        public static AsymmetricCipherKeyPair GetKeyPair()
        {
            CryptoApiRandomGenerator randomGenerator = new CryptoApiRandomGenerator();
            SecureRandom secureRandom = new SecureRandom(randomGenerator);
            var keyGenerationParameters = new KeyGenerationParameters(secureRandom, RsaKeySize);

            var keyPairGenerator = new RsaKeyPairGenerator();
            keyPairGenerator.Init(keyGenerationParameters);
            return keyPairGenerator.GenerateKeyPair();
        }

        public static void GenerateKey(string localPublicEncryptionKeyLocation, string localPrivateEncryptionKeyLocation)
        {
            AsymmetricCipherKeyPair keys = GetKeyPair();
             var publicKey = keys.Public.ToString();

            var textWriter = new StreamWriter(localPrivateEncryptionKeyLocation);
            var pemWriter = new PemWriter(textWriter);
            pemWriter.WriteObject(keys.Private);
            pemWriter.Writer.Flush();
            textWriter.Close();


            textWriter = new StreamWriter(localPublicEncryptionKeyLocation);
            pemWriter = new PemWriter(textWriter);
            pemWriter.WriteObject(keys.Public);
            pemWriter.Writer.Flush();
            textWriter.Close();
        }
        

        public static string Encrypt(string inputMessage)
        {
            UTF8Encoding utf8enc = new UTF8Encoding();
            string cypherText = null;

            try
            {
                // Converting the string message to byte array
                byte[] inputBytes = utf8enc.GetBytes(inputMessage);

                // Creating the RSA algorithm object
                IAsymmetricBlockCipher cipher = new RsaEngine();

                // Initializing the RSA object for Encryption with RSA public key. Remember, for encryption, public key is needed
                cipher.Init(true, publicKey);

                //Encrypting the input bytes
                byte[] cipheredBytes = cipher.ProcessBlock(inputBytes, 0, inputMessage.Length);

                cypherText = Convert.ToBase64String(cipheredBytes);
            }
            catch (Exception)
            {
                // FAILED;
            }

            return cypherText;
        }
        
        public static string Decrypt(string inputMessage)
        {
            UTF8Encoding utf8enc = new UTF8Encoding();
            byte[] encryptedBytes = null;
            string plainText = "";

            try
            {

                // Creating the RSA algorithm object
                IAsymmetricBlockCipher cipher = new RsaEngine();

                // Initializing the RSA object for Decryption with RSA private key. Remember, for decryption, private key is needed
                //cipher.Init(false, KeyPair.Private);
                //cipher.Init(false, KeyPair.Private);
                cipher.Init(false, privateKey);

                
                encryptedBytes = Convert.FromBase64String(inputMessage);

                //Encrypting the input bytes
                //byte[] cipheredBytes = cipher.ProcessBlock(inputBytes, 0, inputMessage.Length);
                byte[] cipheredBytes = cipher.ProcessBlock(encryptedBytes, 0, encryptedBytes.Length);

                //Write the encrypted message to file
                // Write encrypted text to file\
                plainText = Encoding.UTF8.GetString(cipheredBytes);
            }
            catch (Exception)
            {
                // FAILED
            }

            return plainText;
        }

        private static void ExportPrivateKeyToPEMFormat(RSACryptoServiceProvider csp)
        {
            TextWriter outputStream = new StringWriter();

            if (csp.PublicOnly) throw new ArgumentException("CSP does not contain a private key", "csp");
            var parameters = csp.ExportParameters(true);
            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriter(stream);
                writer.Write((byte)0x30); // SEQUENCE
                using (var innerStream = new MemoryStream())
                {
                    var innerWriter = new BinaryWriter(innerStream);
                    EncodeIntegerBigEndian(innerWriter, new byte[] { 0x00 }); // Version
                    EncodeIntegerBigEndian(innerWriter, parameters.Modulus);
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent);
                    EncodeIntegerBigEndian(innerWriter, parameters.D);
                    EncodeIntegerBigEndian(innerWriter, parameters.P);
                    EncodeIntegerBigEndian(innerWriter, parameters.Q);
                    EncodeIntegerBigEndian(innerWriter, parameters.DP);
                    EncodeIntegerBigEndian(innerWriter, parameters.DQ);
                    EncodeIntegerBigEndian(innerWriter, parameters.InverseQ);
                    var length = (int)innerStream.Length;
                    EncodeLength(writer, length);
                    writer.Write(innerStream.GetBuffer(), 0, length);
                }

                var base64 = Convert.ToBase64String(stream.GetBuffer(), 0, (int)stream.Length).ToCharArray();
                outputStream.WriteLine("-----BEGIN RSA PRIVATE KEY-----");
                // Output as Base64 with lines chopped at 64 characters
                for (var i = 0; i < base64.Length; i += 64)
                {
                    outputStream.WriteLine(base64, i, Math.Min(64, base64.Length - i));
                }
                outputStream.WriteLine("-----END RSA PRIVATE KEY-----");

                PEMPrivateKey = outputStream.ToString();
            }
        }

        public static void ExportPublicKeyToPEMFormat(RSACryptoServiceProvider csp)
        {
            TextWriter outputStream = new StringWriter();

            var parameters = csp.ExportParameters(false);
            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriter(stream);
                writer.Write((byte)0x30); // SEQUENCE
                using (var innerStream = new MemoryStream())
                {
                    var innerWriter = new BinaryWriter(innerStream);
                    EncodeIntegerBigEndian(innerWriter, new byte[] { 0x00 }); // Version
                    EncodeIntegerBigEndian(innerWriter, parameters.Modulus);
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent);

                    //All Parameter Must Have Value so Set Other Parameter Value Whit Invalid Data  (for keeping Key Structure  use "parameters.Exponent" value for invalid data)
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent); // instead of parameters.D
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent); // instead of parameters.P
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent); // instead of parameters.Q
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent); // instead of parameters.DP
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent); // instead of parameters.DQ
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent); // instead of parameters.InverseQ

                    var length = (int)innerStream.Length;
                    EncodeLength(writer, length);
                    writer.Write(innerStream.GetBuffer(), 0, length);
                }

                var base64 = Convert.ToBase64String(stream.GetBuffer(), 0, (int)stream.Length).ToCharArray();
                outputStream.WriteLine("-----BEGIN PUBLIC KEY-----");
                // Output as Base64 with lines chopped at 64 characters
                for (var i = 0; i < base64.Length; i += 64)
                {
                    outputStream.WriteLine(base64, i, Math.Min(64, base64.Length - i));
                }
                outputStream.WriteLine("-----END PUBLIC KEY-----");

                PEMPublicKey = outputStream.ToString();

            }
        }

        private static void EncodeIntegerBigEndian(BinaryWriter stream, byte[] value, bool forceUnsigned = true)
        {
            stream.Write((byte)0x02); // INTEGER
            var prefixZeros = 0;
            for (var i = 0; i < value.Length; i++)
            {
                if (value[i] != 0) break;
                prefixZeros++;
            }
            if (value.Length - prefixZeros == 0)
            {
                EncodeLength(stream, 1);
                stream.Write((byte)0);
            }
            else
            {
                if (forceUnsigned && value[prefixZeros] > 0x7f)
                {
                    // Add a prefix zero to force unsigned if the MSB is 1
                    EncodeLength(stream, value.Length - prefixZeros + 1);
                    stream.Write((byte)0);
                }
                else
                {
                    EncodeLength(stream, value.Length - prefixZeros);
                }
                for (var i = prefixZeros; i < value.Length; i++)
                {
                    stream.Write(value[i]);
                }
            }
        }

        private static void EncodeLength(BinaryWriter stream, int length)
        {
            if (length < 0) throw new ArgumentOutOfRangeException("length", "Length must be non-negative");
            if (length < 0x80)
            {
                // Short form
                stream.Write((byte)length);
            }
            else
            {
                // Long form
                var temp = length;
                var bytesRequired = 0;
                while (temp > 0)
                {
                    temp >>= 8;
                    bytesRequired++;
                }
                stream.Write((byte)(bytesRequired | 0x80));
                for (var i = bytesRequired - 1; i >= 0; i--)
                {
                    stream.Write((byte)(length >> (8 * i) & 0xff));
                }
            }
        }

        #endregion

    }
}
