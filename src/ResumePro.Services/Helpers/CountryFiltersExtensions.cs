namespace ResumePro.Services.Helpers;

public static class AppBuilderExtensions
{
    //public static AppBuilder RegisterPdfGeneration(this AppBuilder builder)
    //{
    //    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
    //    {
    //        builder.Services.AddSingleton<IPdfStorage>(
    //            new LocalPdfStorageStrategy(Path.Combine(Environment.CurrentDirectory, "StoredPdfs")));
    //    }
    //    else
    //    {
    //        var blobServiceClient = new BlobServiceClient(builder.Configuration.GetConnectionString("AzureBlobStorage"));
    //        builder.Services.AddSingleton<IPdfStorage>(new AzureBlobPdfStorage(blobServiceClient, "pdfs"));
    //    }

    //    return builder;
    //}
}

//public static class CountryFiltersExtensions
//{
//    public static Expression<Func<Country, bool>> GetExpression(this CountryFilters filters)
//    {
//        var expr = PredicateBuilder.True<Country>();
//        if (filters.Name != null) expr.And(x => x.Name.Contains(filters.Name));
//        return expr;
//    }
//}