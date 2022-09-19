using Compras.Migrations;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Compras.Helpers
{
    public class BlobHelper : IBlobHelper
    {
        private readonly CloudBlobClient _blobClient;
        IConfiguration _Configuration;
        public BlobHelper(IConfiguration configuration)
        {
            string keys = configuration["Blob:ConnectionString"];
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(keys);
            _blobClient = storageAccount.CreateCloudBlobClient();
            _Configuration = configuration;
        }
        public async Task DeleteBlobAsync(Guid id, string containerName, string imagename)
        {
            //try
            //{
            //    CloudBlobContainer container = _blobClient.GetContainerReference(containerName);
            //    CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{id}");
            //    await blockBlob.DeleteAsync();
            //}
            //catch   (Exception)
            //{
            //    throw;
            //}

            string Path = _Configuration["ImagesPath"] + "\\" + containerName + "\\" + imagename;
            
            //Stream stream = File.OpenRead(imagefile);
            if (File.Exists(Path))
            {
                File.Delete(Path);
            }

            //using (FileStream fileStream = System.IO.File.Create(Path))
            //{
            //    fileStream.Write(ReadFully(stream));
            //    fileStream.Close();
            //}

            //return name;
        }

        public async Task<Guid> UploadBlobAsync(IFormFile file, string containerName, string imagename)
        {
            Stream stream = file.OpenReadStream();
            return await UploadBlobAsync(stream, containerName, imagename);

        }

      

        public async Task<Guid> UploadBlobAsync(byte[] file, string containerName, string imagename)
        {
            MemoryStream stream = new MemoryStream(file);
            return await UploadBlobAsync(stream, containerName,  imagename);

        }

        public async Task<Guid> UploadBlobAsync(string imagefile, string containerName, string imagename)
        {
            Stream stream = File.OpenRead(imagefile);
            return await UploadBlobAsync(stream, containerName, imagename);

        }
        private async Task<Guid> UploadBlobAsync(Stream stream, string containerName, string imagename)
        {
            Guid name = Guid.NewGuid();

            string Path = _Configuration["ImagesPath"] + "\\" + containerName + "\\" + imagename;
            if (File.Exists(Path))
            {
                File.Delete(Path);
            }

            using (FileStream fileStream = System.IO.File.Create(Path))
            {
                fileStream.Write(ReadFully(stream));
                fileStream.Close();
            }

            return name;

        }

        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
