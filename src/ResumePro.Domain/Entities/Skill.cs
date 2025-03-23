using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResumePro.Domain.Entities;

public sealed class Skill : BaseEntity<Skill>, ISkill
{
    public ICollection<PersonaSkill> Personas { get; set; } = new List<PersonaSkill>();
    public ICollection<SkillCategorySkill> Categories { get; set; } = new List<SkillCategorySkill>();

    public int Id { get; set; }
    public string Title { get; set; } = null!;

    public override void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.HasKey(x => x.Id);
    }
}