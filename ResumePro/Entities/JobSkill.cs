using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;

namespace ResumePro.Entities;

public class JobSkill : BaseEntity<JobSkill>
{
    public int JobId { get; set; }
    public ResumeJob Job { get; set; }
    public ResumeSkill Skill { get; set; }
    public int ResumeId { get; set; }
    public int SkillId { get; set; }

    public override void Configure(EntityTypeBuilder<JobSkill> builder)
    {
        builder.HasKey(x => new { x.SkillId, x.JobId });

        builder.HasOne(x => x.Job)
            .WithMany(x => x.Skills)
            .HasForeignKey(x => new { x.ResumeId, x.JobId })
            .HasPrincipalKey(x => new { x.ResumeId, x.JobId });

        builder.HasOne(x => x.Skill)
            .WithMany(x => x.Jobs)
            .HasForeignKey(x => new { x.ResumeId, x.SkillId })
            .HasPrincipalKey(x => new { x.ResumeId, x.SkillId });
    }
}