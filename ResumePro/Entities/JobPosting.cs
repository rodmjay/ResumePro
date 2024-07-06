using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;

namespace ResumePro.Entities;

public class JobPosting : BaseEntity<JobPosting>
{
    public int OrganizationId { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public JobCategory JobCategory { get; set; }
    public int JobCategoryId { get; set; }
    public HiringManager HiringManager { get; set; }
    public int HiringManagerId { get; set; }
    public ICollection<Application> Applications { get; set; }

    public override void Configure(EntityTypeBuilder<JobPosting> builder)
    {
        builder.HasKey(x => new { x.OrganizationId, x.Id });

        builder.HasOne(x => x.JobCategory)
            .WithMany(x => x.JobPostings)
            .HasForeignKey(x => x.JobCategoryId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.HiringManager)
            .WithMany(x => x.JobPostings)
            .HasForeignKey(x => new { x.OrganizationId, x.HiringManagerId })
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Company)
            .WithMany(x => x.JobPostings)
            .HasForeignKey(x => new { x.OrganizationId, x.CompanyId })
            .HasPrincipalKey(x => new { x.OrganizationId, x.Id })
            .OnDelete(DeleteBehavior.NoAction);
    }
}