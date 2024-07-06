using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;

namespace ResumePro.Entities
{
    public class JobCategory : BaseEntity<JobCategory>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<JobPosting> JobPostings { get; set; }
        public override void Configure(EntityTypeBuilder<JobCategory> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
