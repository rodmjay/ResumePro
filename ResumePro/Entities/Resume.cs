using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;
using ResumePro.Shared;

namespace ResumePro.Entities;

public class Resume : BaseEntity<Resume>, IResume
{
    public Persona Persona { get; set; }
    public int PersonaId { get; set; }
    public ICollection<ResumeJob> Jobs { get; set; }
    public ICollection<ResumeSkill> Skills { get; set; }
    public int Id { get; set; }
    public string ShortName { get; set; }
    public string Description { get; set; }

    public override void Configure(EntityTypeBuilder<Resume> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Persona)
            .WithMany(x => x.Resumes)
            .HasForeignKey(x => x.PersonaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}