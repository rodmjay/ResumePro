using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;

namespace ResumePro.Entities;

public class ResumeJob : BaseEntity<ResumeJob>
{
    public int ResumeId { get; set; }
    public Resume Resume { get; set; }
    public int PersonaId { get; set; }
    public Persona Persona { get; set; }

    public int JobId { get; set; }
    public Job Job { get; set; }
    public ICollection<JobSkill> Skills { get; set; } = new List<JobSkill>();

    public int Order { get; set; }

    public override void Configure(EntityTypeBuilder<ResumeJob> builder)
    {
        builder.HasKey(x => new {x.ResumeId, x.JobId});

        builder.HasOne(x => x.Resume)
            .WithMany(x => x.Jobs)
            .HasForeignKey(x => new{ x.PersonaId, x.ResumeId})
            .HasPrincipalKey(x => new{ x.PersonaId, x.Id})
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Job)
            .WithMany(x => x.Resumes)
            .HasForeignKey(x => x.JobId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}