using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResumePro.Domain.Entities;

public sealed class ResumeCompany : BaseEntity<ResumeCompany>
{
    public int OrganizationId { get; set; }
    public int ResumeId { get; set; }
    public Resume Resume { get; set; } = null!;
    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;
    public int PersonId { get; set; }

    public override void Configure(EntityTypeBuilder<ResumeCompany> builder)
    {
        builder.HasKey(x => new { x.OrganizationId, x.ResumeId, x.CompanyId });

        builder.HasOne(x => x.Resume)
            .WithMany(x => x.Companies)
            .HasForeignKey(x => new { x.OrganizationId, x.ResumeId, x.PersonId })
            .HasPrincipalKey(x => new { x.OrganizationId, x.Id, x.PersonId })
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Company)
            .WithMany(x => x.Resumes)
            .HasForeignKey(x => new { x.OrganizationId, x.CompanyId, x.PersonId })
            .HasPrincipalKey(x => new { x.OrganizationId, x.Id, x.PersonId })
            .OnDelete(DeleteBehavior.Cascade);
    }
}
