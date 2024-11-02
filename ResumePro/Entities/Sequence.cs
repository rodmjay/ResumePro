using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;

namespace ResumePro.Entities
{
    public class Sequence : BaseEntity<Sequence>
    {
        public int OrganizationId { get; set; }
        public int ResumeId { get; set; }
        public override void Configure(EntityTypeBuilder<Sequence> builder)
        {
            builder.HasKey(x => x.OrganizationId);
        }
    }
}
