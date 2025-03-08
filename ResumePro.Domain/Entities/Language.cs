#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResumePro.Domain.Entities;

public class Language : BaseEntity<Language>, ILanguage
{
    public string NativeName { get; set; }

    public ICollection<PersonaLanguage> People { get; set; }
    public string Name { get; set; }
    public string Code2 { get; set; }
    public string Code3 { get; set; }

    public override void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.HasKey(x => x.Code3);
    }
}