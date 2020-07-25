using System;
using System.Security.Cryptography;
using System.Text;

namespace YOY.Security
{
    public class Checksums
    {
        /// <summary>
        /// Calcula el valor del digesto de un mensaje dado y retorna el resultado en una cadena de caracteres codificada 
        /// en base 64. Antes de realizar el cálculo del hash, este método crea un grupo de salt bytes utilizando un generador
        /// de números pseudoaleatorios para poder proveer un grupo de bytes criptográficamente fuertes que permiten diferenciar
        /// digestos calculados en distintos contextos.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="algorithm"></param>
        /// <param name="saltBytes"></param>
        /// <returns></returns>
        public String Compute(String data, DigestAlgorithm algorithm, byte[] saltBytes)
        {
            if (saltBytes == null)
            {
                saltBytes = this.GenerateSalt();

            } // IF ENDS

            // Convierte todo el texto plano en arreglos de bytes
            Byte[] plainTextBytes = Encoding.UTF8.GetBytes(data);

            Byte[] plaintTextWithSalt = new Byte[plainTextBytes.Length + saltBytes.Length];

            // Se realiza la copia de los bytes de data y los salt
            for (int i = 0; i < plainTextBytes.Length; i++)
                plaintTextWithSalt[i] = plainTextBytes[i];

            for (int e = 0; e < saltBytes.Length; e++)
                plaintTextWithSalt[plaintTextWithSalt.Length + e] = saltBytes[e];

            // Calcula el valor del digesto
            Byte[] hash = this.ComputeDigest(algorithm, plaintTextWithSalt);

            // Crea un arreglo con todos los bytes del digesto y los bytes de Salt
            Byte[] hashWithSalt = new Byte[hash.Length + saltBytes.Length];

            for (int a = 0; a < hash.Length; a++)
                hashWithSalt[a] = hash[a];

            for (int o = 0; o < saltBytes.Length; o++)
                hashWithSalt[hash.Length + o] = saltBytes[o];

            return Convert.ToBase64String(hashWithSalt);

        } // Método Compute Ends

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="algorithm"></param>
        /// <returns></returns>
        public String Compute(String data, DigestAlgorithm algorithm)
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            byte[] hash = this.ComputeDigest(algorithm, dataBytes);
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alg"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private Byte[] ComputeDigest(DigestAlgorithm alg, byte[] data)
        {
            // Como se soportan múltiples algoritmos para calcular el digesto del mensaje, se va a utilizar
            // la clase HashAlgorithm como una clase abstracta común que encapsula la funcionalidad de las 
            // distintas implementaciones.
            HashAlgorithm hash;

            switch (alg)
            {
                case DigestAlgorithm.MD5:
                    hash = new MD5CryptoServiceProvider();
                    break;
                case DigestAlgorithm.SHA1:
                    hash = new SHA1Managed();
                    break;
                case DigestAlgorithm.SHA256:
                    hash = new SHA256Managed();
                    break;
                case DigestAlgorithm.SHA384:
                    hash = new SHA384Managed();
                    break;
                case DigestAlgorithm.SHA512:
                    hash = new SHA512Managed();
                    break;
                default:
                    hash = new MD5CryptoServiceProvider();
                    break;
            } //SWITCH ENDS

            byte[] hashBytes = hash.ComputeHash(data);

            return hashBytes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] GenerateSalt()
        {
            byte[] saltBytes;

            Int32 minSaltSize = 4;
            Int32 maxSaltSize = 8;

            // Se crea una instancia de un generador de números pseudoaleatorios
            Random random = new Random();
            Int32 saltSize = random.Next(minSaltSize, maxSaltSize);

            // Se colocan los bytes SALT en un arreglo 
            saltBytes = new Byte[saltSize];

            // Inicializa un generador de número aleatorios
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            // Rellena el arreglo de bytes con número aleatorios 
            rng.GetNonZeroBytes(saltBytes);

            return saltBytes;

        } // GENERATE SALT BYTES ENDS

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plainData"></param>
        /// <param name="alg"></param>
        /// <param name="hashValue"></param>
        /// <returns></returns>
        public Boolean Verify(String plainData, DigestAlgorithm alg, String hashValue)
        {
            Boolean isValid = false;

            byte[] hashWithSalt = Convert.FromBase64String(hashValue);
            Int32 hashSizeInBits, hashSizeInBytes;

            switch (alg)
            {
                case DigestAlgorithm.MD5:
                    hashSizeInBits = 128;
                    break;
                case DigestAlgorithm.SHA1:
                    hashSizeInBits = 160;
                    break;
                case DigestAlgorithm.SHA256:
                    hashSizeInBits = 256;
                    break;
                case DigestAlgorithm.SHA384:
                    hashSizeInBits = 384;
                    break;
                case DigestAlgorithm.SHA512:
                    hashSizeInBits = 512;
                    break;
                default:
                    hashSizeInBits = 128;
                    break;
            } // SWITCH ENDS

            hashSizeInBytes = hashSizeInBits / 8;

            if (!(hashWithSalt.Length < hashSizeInBytes))
            {
                // Se crear un arreglo del tamaño del Salt Calculado a partir del tamaño del Hash
                byte[] saltBytes = new byte[hashWithSalt.Length - hashSizeInBytes];

                // Se copial los bytes del SALT
                for (int i = 0; i < saltBytes.Length; i++)
                    saltBytes[i] = hashWithSalt[hashSizeInBytes + i];

                String expectedHash = this.Compute(plainData, alg, saltBytes);

                if (hashValue == expectedHash)
                    isValid = true;

            } // IF ENDS
            else
            {
                isValid = false;
            } // ELSE ENDS

            return isValid;
        }
    } // CLASS CHECKSUM ENDS
    public enum DigestAlgorithm
    {
        MD5,
        SHA1,
        SHA256,
        SHA384,
        SHA512
    }
}
