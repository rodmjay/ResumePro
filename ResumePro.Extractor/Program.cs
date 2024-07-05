using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResumePro.Context;
using ResumePro.Core.Middleware.Extensions;
using ResumePro.Extensions;
using System.Reflection.PortableExecutable;
using PdfSharp.Pdf.IO;
using System.Text;
using Newtonsoft.Json;
using PdfSharp.Pdf;


namespace ResumePro.Extractor
{
    internal class Program
    {
        private static readonly IServiceProvider ServiceProvider;

        static Program()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            var services = new ServiceCollection();

            ServiceProvider = services!.ConfigureApp(configuration)
                .AddDatabase<ApplicationContext>()
                .AddAutomapperProfilesFromAssemblies()
                .AddApplicationDependencies()
                .Build();
        }

        static void Main(string[] args)
        {
            var fileName = $"Rod Johnson-Enterprise Application Architect.pdf";
            var relativePath = $@"..\..\..\..\{fileName}";
            var absolutePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath));

            PdfDocument doc = PdfReader.Open(absolutePath, PdfDocumentOpenMode.Import);

            var stringBuilder = new StringBuilder();

            foreach (var page in doc.Pages)
            {
                var text = page.ExtractText();

                foreach (var item in text)
                {
                    stringBuilder.AppendLine(item);
                }
            }
            
            var parser = new ResumeParser();

            var resume = parser.Parse(stringBuilder.ToString());

            var serialized = JsonConvert.SerializeObject(resume, Formatting.Indented);

            Console.Write(serialized);

            Console.ReadLine();
        }
       
    }
}
