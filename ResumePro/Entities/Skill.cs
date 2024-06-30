using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using ResumePro.Core.Data.Bases;
using ResumePro.Shared;

namespace ResumePro.Entities;

public class Skill : BaseEntity<Skill>, ISkill
{
    public int Id { get; set; }
    public string Title { get; set; }
    public ICollection<PersonaSkill> Personas { get; set; }

    public override void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.HasKey(x => x.Id);
    }
}