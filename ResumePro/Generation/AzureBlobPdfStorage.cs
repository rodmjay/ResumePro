using Azure.Storage.Blobs;

namespace ResumePro.Generation;

public class AzureBlobPdfStorage(BlobServiceClient blobServiceClient, string containerName) : IPdfStorage
{
    public async Task<string> SavePdfAsync(Stream pdfStream, string fileName)
    {
        var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = blobContainerClient.GetBlobClient(fileName);
        await blobClient.UploadAsync(pdfStream, overwrite: true);
        return blobClient.Uri.AbsoluteUri;

    }
}