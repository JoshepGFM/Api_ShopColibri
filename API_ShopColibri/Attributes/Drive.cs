using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util;
using Google.Apis.Util.Store;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace API_ShopColibri.Attributes
{
    public class Drive
    {
        private string[] Scopes = { DriveService.Scope.Drive };
        private string ApplicationName = "ShopColibriApp";
        private UserCredential credential;

        private DriveService GetService()
        {

            using (var stream = new FileStream("Properties/CredenDri.json", FileMode.Open, FileAccess.Read))
            {
                string creadPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(creadPath, true)).Result;
            }

            if (credential.Token.IsExpired(credential.Flow.Clock))
            {
                if (credential.RefreshTokenAsync(CancellationToken.None).Result)
                {
                    Console.WriteLine("Token de acceso actualizado.");
                }
                else
                {
                    Console.WriteLine("Error al actualizar el token de acceso.");
                }
            }
            else
            {
                Console.WriteLine("El token de acceso aún es válido.");
            }

            //var service = new DriveService(new BaseClientService.Initializer()
            //{
            //    HttpClientInitializer = credential,
            //    ApplicationName = ApplicationName,
            //});
            //Crear el servicio de Google Drive usando el token actualizado
            var service = new DriveService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            return service;
        }

        public void RefreshToken()
        {
            //GetService();
        }

        public string ValidarFolder()
        {
            DriveService service = GetService();
            FilesResource.ListRequest listResquest = service.Files.List();
            listResquest.PageSize = 100;
            listResquest.Fields = "nextPageToken, files(*)";

            IList<Google.Apis.Drive.v3.Data.File> files = listResquest.Execute().Files;
            if(files != null && files.Count > 0)
            {
                string folder = files.Where(x => x.Name == "ImagIventario" && x.MimeType == "application/vnd.google-apps.folder" && x.Trashed == false).Select(z => z.Id).FirstOrDefault();
                if (folder == null)
                {
                    folder = CrearCarpeta("ImagIventario");
                }
                return folder;
            }
            return null;
        }

        private string CrearCarpeta(string FolderName)
        {
            DriveService service = GetService();
            var FileMetaData = new Google.Apis.Drive.v3.Data.File();
            FileMetaData.Name = FolderName;
            FileMetaData.MimeType = "application/vnd.google-apps.folder";
            FilesResource.CreateRequest request;
            request = service.Files.Create(FileMetaData);
            request.Fields = "id";
            var file = request.Execute();

            return file.Id;
        }

        public void ObtenerArchivos()
        {
            DriveService service = GetService();

            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 100;
            listRequest.Fields = "nextPageToken, files(*)";

            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;
            Console.WriteLine("Files:");
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    Console.WriteLine("{0} ({1})", file.Name, file.Id);
                }
            }
            else
            {
                Console.WriteLine("No files found.");
            }
        }

        public string SubirArchivo(string patch, string IdFolder)
        {
            DriveService service = GetService();
            IList<string> ListaFolder = new List<string>();
            ListaFolder.Add(IdFolder);
            var fileMetadata = new Google.Apis.Drive.v3.Data.File();
            fileMetadata.Name = "" + Path.GetFileName(patch);
            fileMetadata.MimeType = "application/octet-stream";
            fileMetadata.Parents = ListaFolder;
            FilesResource.CreateMediaUpload request;
            using (var stream = new System.IO.FileStream(patch, System.IO.FileMode.Open))
            {
                request = service.Files.Create(fileMetadata, stream, "application/octet-stream");
                request.Fields = "id";
                request.Upload();
            }

            var file = request.ResponseBody;

            string enlaceDescarga = file.Id;

            return enlaceDescarga;
        }

        public void EliminarImagen(string fileId)
        {
            DriveService service = GetService();

            // Eliminar el archivo de Google Drive
            service.Files.Delete(fileId).Execute();
        }
    }
}
