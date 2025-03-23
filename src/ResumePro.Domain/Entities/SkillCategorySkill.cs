using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResumePro.Domain.Entities;

public sealed class SkillCategorySkill : BaseEntity<SkillCategorySkill>
{
    public int SkillId { get; set; }
    public Skill Skill { get; set; } = null!;

    public int SkillCategoryId { get; set; }
    public SkillCategory SkillCategory { get; set; } = null!;

    public override void Configure(EntityTypeBuilder<SkillCategorySkill> builder)
    {
        builder.HasKey(x => new { x.SkillCategoryId, x.SkillId });

        builder.HasOne(x => x.Skill)
            .WithMany(x => x.Categories)
            .HasForeignKey(x => x.SkillId);

        builder.HasOne(x => x.SkillCategory)
            .WithMany(x => x.Skills)
            .HasForeignKey(x => x.SkillCategoryId);
    }
}