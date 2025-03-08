namespace ResumePro.Generation
{
    public class LocalPdfStorageStrategy : IPdfStorage
    {
        private readonly string _basePath;

        public LocalPdfStorageStrategy(string basePath)
        {
            _basePath = basePath;
        }

        public async Task<string> SavePdfAsync(Stream pdfStream, string fileName)
        {
            var filePath = Path.Combine(_basePath, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await pdfStream.CopyToAsync(fileStream);
            }

            return filePath;
        }
    }
}
