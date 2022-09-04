namespace Compras.Helpers
{
    public interface IBlobHelper
    {
        Task<Guid> UploadBlobAsync(IFormFile file, string containerName, string imagename);

        Task<Guid> UploadBlobAsync(byte[] file, string containerName, string imagename);

        Task<Guid> UploadBlobAsync(string image, string containerName, string imagename);

        Task DeleteBlobAsync(Guid id, string containerName, string imagename);

    }
}
