using Azure.Storage.Blobs;

namespace ResumePro.Generation;

public class AzureBlobPdfStorage : IPdfStorage
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly string _containerName;

    public AzureBlobPdfStorage(BlobServiceClient blobServiceClient, string containerName)
    {
        _blobServiceClient = blobServiceClient;
        _containerName = containerName;
    }

    public async Task<string> SavePdfAsync(Stream pdfStream, string fileName)
    {
        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        var blobClient = blobContainerClient.GetBlobClient(fileName);
        await blobClient.UploadAsync(pdfStream, overwrite: true);
        return blobClient.Uri.AbsoluteUri;

    }
}