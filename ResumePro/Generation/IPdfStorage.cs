namespace ResumePro.Generation;

public interface IPdfStorage
{
    Task<string> SavePdfAsync(Stream pdfStream, string fileName);
}