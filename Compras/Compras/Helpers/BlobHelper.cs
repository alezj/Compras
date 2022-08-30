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
        public async Task DeleteBlobAsync(Guid id, string containerName)
        {
            try
            {
                CloudBlobContainer container = _blobClient.GetContainerReference(containerName);
                CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{id}");
                await blockBlob.DeleteAsync();
            }
            catch   (Exception)
            {
                throw;
            }
        }

        public async Task<Guid> UploadBlobAsync(IFormFile file, string containerName)
        {
            Stream stream = file.OpenReadStream();
            return await UploadBlobAsync(stream, containerName);

        }

      

        public async Task<Guid> UploadBlobAsync(byte[] file, string containerName)
        {
            MemoryStream stream = new MemoryStream(file);
            return await UploadBlobAsync(stream, containerName);

        }

        public async Task<Guid> UploadBlobAsync(string image, string containerName)
        {
            Stream stream = File.OpenRead(image);
            return await UploadBlobAsync(stream, containerName);

        }
        private async Task<Guid> UploadBlobAsync(Stream stream, string containerName)
        {
            Guid name = Guid.NewGuid();

            string Path = _Configuration["ImagesPath"] + "\\" + containerName;
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
