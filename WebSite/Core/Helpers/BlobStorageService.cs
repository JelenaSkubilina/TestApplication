using ImageMagick;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;

namespace WebSite.Core.Helpers
{
    public class BlobStorageService
    {
        private readonly string accessKey;

        public BlobStorageService(string accessKey)
        {
            this.accessKey = accessKey;
        }

        public string UploadFileToBlob(IFormFile file)
        {
            try
            {

                var _task = Task.Run(() => this.UploadFileToBlobAsync(file));
                _task.Wait();
                string fileUrl = _task.Result;
                return fileUrl;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async void DeleteBlobData(string fileUrl)
        {
            var uriObj = new Uri(fileUrl);

            string BlobName = Path.GetFileName(uriObj.LocalPath);

            var cloudBlobContainer = CreateCloudBlobContainer();
            var blockBlob = cloudBlobContainer.GetBlobReference(BlobName);
      
            await blockBlob.DeleteAsync();
        }

        private CloudBlobContainer CreateCloudBlobContainer()
        {
            var cloudStorageAccount = CloudStorageAccount.Parse(accessKey);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            var cloudBlobContainer = cloudBlobClient.GetContainerReference("uploads");

            return cloudBlobContainer;
        }

        private async Task<string> UploadFileToBlobAsync(IFormFile file)
        {
            try
            {
                var cloudBlobContainer = CreateCloudBlobContainer();

                if (await cloudBlobContainer.CreateIfNotExistsAsync())
                {
                    await cloudBlobContainer.SetPermissionsAsync(
                        new BlobContainerPermissions 
                        {
                            PublicAccess = BlobContainerPublicAccessType.Blob 
                        });
                }

                string fileName = Path.GetFileName(file.FileName);

                var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);

                cloudBlockBlob.Properties.ContentType = file.ContentType;

                if (file.ContentType.Length > 0 && file.ContentType.Contains("image"))
                    await cloudBlockBlob.UploadFromFileAsync(ResizeImage(file).FullName);
                else
                    await cloudBlockBlob.UploadFromStreamAsync(file.OpenReadStream());

                return cloudBlockBlob.Uri.AbsoluteUri;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        private FileInfo ResizeImage(IFormFile file)
        {
            var resizedImage = new MagickImage(file.OpenReadStream());
            var size = new MagickGeometry(200, 200);

            resizedImage.Resize(size);

            var fileInfo = new FileInfo(file.FileName);

            resizedImage.Write(fileInfo);

            return fileInfo;
        }
    }
}

