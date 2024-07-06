using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;
using ResumePro.Shared.Enums;

namespace ResumePro.Entities
{

    public class Application : BaseEntity<Application>
    {
        public int OrganizationId { get; set; }
        public int Id { get; set; }
        public JobPosting JobPosting { get; set; }
        public int JobPostingId { get; set; }
        public int PersonaId { get; set; }
        public Persona Persona { get; set; }
        public ApplicationStatus ApplicationStatus { get; set; }

        public Resume Resume { get; set; }
        public int ResumeId { get; set; }
        public DateTime ApplicationDate { get; set; }
 
        public override void Configure(EntityTypeBuilder<Application> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.JobPosting)
                .WithMany(x => x.Applications)
                .HasForeignKey(x => new {x.OrganizationId, x.JobPostingId})
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Persona)
                .WithMany(x => x.Applications)
                .HasForeignKey(x => new {x.OrganizationId, x.PersonaId})
                .HasPrincipalKey(x => new {x.OrganizationId, x.Id})
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Resume)
                .WithMany(x => x.Applications)
                .HasForeignKey(x => new {x.OrganizationId, x.ResumeId})
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
