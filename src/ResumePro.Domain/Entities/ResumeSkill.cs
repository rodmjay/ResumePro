using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResumePro.Domain.Entities;

public sealed class ResumeSkill : BaseEntity<ResumeSkill>, IResumeSkill
{
    public Resume Resume { get; set; } = null!;
    public PersonaSkill Skill { get; set; } = null!;
    public int OrganizationId { get; set; }
    public int ResumeId { get; set; }
    public int SkillId { get; set; }
    public int PersonId { get; set; }

    public override void Configure(EntityTypeBuilder<ResumeSkill> builder)
    {
        builder.HasKey(x => new { x.OrganizationId, x.PersonId, x.ResumeId, x.SkillId });

        builder.HasOne(x => x.Resume)
            .WithMany(x => x.Skills)
            .HasForeignKey(x => new { x.OrganizationId, x.PersonId, x.ResumeId })
            .HasPrincipalKey(x => new { x.OrganizationId, x.PersonId, x.Id })
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Skill)
            .WithMany(x => x.Resumes)
            .HasForeignKey(x => new { x.OrganizationId, x.PersonId, x.SkillId })
            .HasPrincipalKey(x => new { x.OrganizationId, x.PersonId, x.SkillId })
            .OnDelete(DeleteBehavior.Cascade);
    }
}