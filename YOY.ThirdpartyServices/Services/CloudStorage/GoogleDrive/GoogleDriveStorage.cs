using YOY.DTO.Entities;
using YOY.DTO.Entities.Misc.Files;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace YOY.ThirdpartyServices.Services.CloudStorage.GoogleDrive
{
    public class GoogleDriveStorage
    {
        private static string[] Scopes = { DriveService.Scope.DriveReadonly };
        private static string ApplicationName = "Drive API YOY";
        private static DriveService _service;


        private static void Initialize(byte[] certData, string serviceAccount)
        {
            if (_service == null)
            {
                string[] scopes = new string[] { DriveService.Scope.Drive }; // Full access

                //loading the Key file
                var certificate = new X509Certificate2(certData, "notasecret", X509KeyStorageFlags.MachineKeySet |
                                                        X509KeyStorageFlags.PersistKeySet |
                                                        X509KeyStorageFlags.Exportable);
                var credential = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(serviceAccount)
                {
                    Scopes = scopes
                }.FromCertificate(certificate));

                // Create Drive API service.
                _service = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });
            }
        }

        private static string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);

            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();

            return mimeType;
        }

        private static byte[] StreamToByteArray(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static string CreateFolder(string keyPath, string serviceAccount, string folderName)
        {
            string folderId;
            try
            {
                //Creates a folder
                var fileMetadata = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = folderName,
                    MimeType = "application/vnd.google-apps.folder"
                };
                var request = _service.Files.Create(fileMetadata);
                request.Fields = "id";
                var file = request.Execute();
                folderId = file.Id;
            }
            catch (Exception)
            {
                folderId = "";
            }


            return folderId;
        }

        public static FileStorageData UploadFile(string folderId, string filePath)
        {
            FileStorageData fileData = new FileStorageData();

            try
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                Stream keyStream = assembly.GetManifestResourceStream("EnjoyIt.ThirdpartyServices.Resources.Keys.enjoyit-af2930ada615.p12");
                var certData = StreamToByteArray(keyStream);

                //string keyPath = AppDomain.CurrentDomain.BaseDirectory;
                //keyPath = keyPath.Replace(baseProject, "ThirdpartyServices") + "Services\\CloudStorage\\Keys\\GoogleDrive\\enjoyit-af2930ada615.p12";

                Initialize(certData, Settings.Default.drive_service_account);

                string mimeType = GetMimeType(filePath);

                if (_service != null)
                {
                    Google.Apis.Drive.v3.Data.File file = new Google.Apis.Drive.v3.Data.File
                    {
                        Name = Path.GetFileName(filePath),
                        Description = "YOY source",
                        MimeType = mimeType,
                        Parents = new List<string>
                    {
                        folderId
                    }
                    };


                    var uploadedFile = (dynamic)null;
                    byte[] byteArray = System.IO.File.ReadAllBytes(filePath);
                    MemoryStream stream = new MemoryStream(byteArray);
                    try
                    {
                        FilesResource.CreateMediaUpload request = _service.Files.Create(file, stream, mimeType);
                        request.Upload();
                        uploadedFile = request.ResponseBody;

                        fileData.FileId = uploadedFile.Id;
                        fileData.MimeType = mimeType;
                    }
                    catch (Exception e)
                    {
                        fileData.FileId = "";
                        fileData.MimeType = "";
                        fileData.Message = e.Message;
                    }
                }
                else
                {
                    fileData.FileId = "";
                    fileData.MimeType = "";
                    fileData.Message = "Google Drive Service isn't initialized";
                }

            }
            catch (Exception e)
            {
                fileData.FileId = "";
                fileData.MimeType = "";
                fileData.Message = e.Message;
            }

            return fileData;
        }

        public static FileContent DownloadFile(string fileId)
        {
            FileContent downloadedFile = new FileContent();


            try
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                Stream stream = assembly.GetManifestResourceStream("EnjoyIt.ThirdpartyServices.Resources.Keys.enjoyit-af2930ada615.p12");
                var certData = StreamToByteArray(stream);

                //string keyPath = AppDomain.CurrentDomain.BaseDirectory;
                //keyPath = keyPath.Replace(baseProject, "ThirdpartyServices") + "Services\\CloudStorage\\Keys\\GoogleDrive\\enjoyit-af2930ada615.p12";

                Initialize(certData, Settings.Default.drive_service_account);

                var request = _service.Files.Get(fileId);

                Google.Apis.Drive.v3.Data.File currentFile = request.Execute();

                //Extract file data
                downloadedFile.Name = currentFile.Name;
                downloadedFile.MimeType = currentFile.MimeType;
                downloadedFile.Id = currentFile.Id;
                downloadedFile.CreationDate = currentFile.CreatedTime;
                downloadedFile.Content = new MemoryStream();


                string status;


                // Add a handler which will be notified on progress changes.
                // It will notify on each chunk download and when the
                // download is completed or failed.
                request.MediaDownloader.ProgressChanged +=
                    (IDownloadProgress progress) =>
                    {
                        switch (progress.Status)
                        {
                            case DownloadStatus.Downloading:
                                {
                                    status = progress.BytesDownloaded + "";
                                    break;
                                }
                            case DownloadStatus.Completed:
                                {
                                    status = "Download complete.";
                                    break;
                                }
                            case DownloadStatus.Failed:
                                {
                                    status = "Download failed.";
                                    break;
                                }
                        }
                    };
                request.Download(downloadedFile.Content);

            }
            catch (Exception)
            {
                downloadedFile = null;
            }

            return downloadedFile;
        }

        public static FileContent GetFileData(string fileId)
        {
            FileContent downloadedFile = new FileContent();


            try
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                Stream stream = assembly.GetManifestResourceStream("EnjoyIt.ThirdpartyServices.Resources.Keys.enjoyit-af2930ada615.p12");
                var certData = StreamToByteArray(stream);

                //string keyPath = AppDomain.CurrentDomain.BaseDirectory;
                //keyPath = keyPath.Replace(baseProject, "ThirdpartyServices") + "Services\\CloudStorage\\Keys\\GoogleDrive\\enjoyit-af2930ada615.p12";

                Initialize(certData, Settings.Default.drive_service_account);

                var request = _service.Files.Get(fileId);

                Google.Apis.Drive.v3.Data.File currentFile = request.Execute();

                //Extract file data
                downloadedFile.Name = currentFile.Name;
                downloadedFile.MimeType = currentFile.MimeType;
                downloadedFile.Id = currentFile.Id;
                downloadedFile.CreationDate = currentFile.CreatedTime;
                downloadedFile.Content = null;

            }
            catch (Exception)
            {
                downloadedFile = null;
            }

            return downloadedFile;
        }

        public static bool DeleteFile(string fileId)
        {
            bool success;
            try
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                Stream stream = assembly.GetManifestResourceStream("YOY.ThirdpartyServices.Resources.Keys.enjoyit-af2930ada615.p12");
                var certData = StreamToByteArray(stream);

                Initialize(certData, Settings.Default.drive_service_account);

                _service.Files.Delete(fileId).Execute();

                success = true;
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }

    }
}
