namespace ResumePro.Generation
{
    public class LocalPdfStorageStrategy(string basePath) : IPdfStorage
    {
        public async Task<string> SavePdfAsync(Stream pdfStream, string fileName)
        {
            var filePath = Path.Combine(basePath, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await pdfStream.CopyToAsync(fileStream);
            }

            return filePath;
        }
    }
}
