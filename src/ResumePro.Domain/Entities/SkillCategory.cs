using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResumePro.Domain.Entities;

public sealed class SkillCategory : BaseEntity<SkillCategory>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<SkillCategorySkill> Skills { get; set; } = new List<SkillCategorySkill>();

    public override void Configure(EntityTypeBuilder<SkillCategory> builder)
    {
        builder.HasKey(x => x.Id);
    }
}