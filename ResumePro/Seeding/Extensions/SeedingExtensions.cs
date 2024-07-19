#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Globalization;
using System.Reflection;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Context;
using ResumePro.Shared.Enums;

namespace ResumePro.Seeding.Extensions;

public static class SeedingExtensions
{
    private static readonly string _seederPath;
    private static readonly Assembly _assembly;

    static SeedingExtensions()
    {
        _assembly = typeof(ApplicationContext).GetTypeInfo().Assembly;
        _seederPath = $"{_assembly.GetName().Name}.Seeding.csv";
    }

    private static string GetResourceFilename(string resouce)
    {
        return $"{_seederPath}.{resouce}";
    }

    private static CsvReader GetReader(StreamReader reader)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            HeaderValidated = null,
            MissingFieldFound = null,
            UseNewObjectForNullReferenceMembers = false,
            IgnoreReferences = true
        };

        var csvReader = new CsvReader(reader, config);

        csvReader.Context.TypeConverterOptionsCache.GetOptions<string>().NullValues.Add("NULL");
        csvReader.Context.TypeConverterOptionsCache.GetOptions<int?>().NullValues.Add("NULL");
        csvReader.Context.TypeConverterOptionsCache.GetOptions<DateTime?>().NullValues.Add("NULL");
        csvReader.Context.TypeConverterOptionsCache.GetOptions<decimal?>().NullValues.Add("NULL");
        csvReader.Context.TypeConverterOptionsCache.GetOptions<bool>().BooleanFalseValues.Add("0");
        csvReader.Context.TypeConverterOptionsCache.GetOptions<bool>().BooleanTrueValues.Add("1");
        csvReader.Context.TypeConverterOptionsCache.GetOptions<bool?>().NullValues.Add("NULL");
        csvReader.Context.TypeConverterOptionsCache.GetOptions<bool?>().BooleanFalseValues.Add("0");
        csvReader.Context.TypeConverterOptionsCache.GetOptions<bool?>().BooleanTrueValues.Add("1");
        csvReader.Context.TypeConverterOptionsCache.GetOptions<SkillView?>().NullValues.Add("NULL");

        return csvReader;
    }

    public static void SeedTemplates(this EntityTypeBuilder<Template> builder, string folder)
    {
        var directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folder);

        if (!Directory.Exists(directoryPath)) throw new Exception("Folder does not exist");

        var templates = new List<Template>();

        var files = Directory.GetFiles(directoryPath);

        for (var index = 0; index < files.Length; index++)
        {
            var filePath = files[index];
            var fileContent = File.ReadAllText(filePath);

            var rawName = Path.GetFileNameWithoutExtension(filePath);

            var nameParts = rawName.Split(".");
            var idPart = nameParts[0].Split("_")[0];
            var namePart = nameParts[0].Split("_")[1];

            templates.Add(new Template
            {
                Id = int.Parse(idPart),
                Name = namePart,
                Source = fileContent,
                Format = nameParts[1],
                Engine = Path.GetExtension(filePath).Replace(".", "")
            });
        }

        builder.HasData(templates);
    }

    public static void Seed<TEntity>(this EntityTypeBuilder<TEntity> builder, string fileName)
        where TEntity : class
    {
        var file = GetResourceFilename(fileName);
        using (var stream = _assembly.GetManifestResourceStream(file))
        {
            if (stream == null) return;
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                var csvReader = GetReader(reader);

                var records = csvReader.GetRecords<TEntity>().ToList();
                builder.HasData(records);
            }
        }
    }
}