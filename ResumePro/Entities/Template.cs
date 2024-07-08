using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;

namespace ResumePro.Entities
{
    public class Template : BaseEntity<Template>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }
        public string Format { get; set; }

        public override void Configure(EntityTypeBuilder<Template> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
