#region Header

// /*

// Author: Rod Johnson, Architect, rodmjay@gmail.com
// */

#endregion

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;
using ResumePro.Entities;
using ResumePro.Languages.Interfaces;

namespace ResumePro.Languages.Entities
{
    public class Language : BaseEntity<Language>, ILanguage
    {
        public string Name { get; set; }
        public string NativeName { get; set; }
        public string Code2 { get; set; }
        public string Code3 { get; set; }

        public ICollection<PersonaLanguage> People { get; set; }

        public override void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.HasKey(x => x.Code3);
        }
    }
}