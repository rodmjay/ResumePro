using Bespoke.Data.Enums;
using Bespoke.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Entities;

namespace ResumePro.Domain.Entities;

public sealed class Persona : BaseEntity<Persona>, IPersona
{
    public ICollection<Highlight> Highlights { get; set; } = new List<Highlight>();
    public ICollection<PersonaSkill> Skills { get; set; } = new List<PersonaSkill>();
    public ICollection<Resume> Resumes { get; set; } = new List<Resume>();
    public ICollection<Company> Companies { get; set; } = new List<Company>();
    public ICollection<School> Schools { get; set; } = new List<School>();
    public ICollection<Certification> Certifications { get; set; } = new List<Certification>();
    public ICollection<PersonaLanguage> Languages { get; set; } = new List<PersonaLanguage>();
    public StateProvince State { get; set; } = null!;
    public bool IsDeleted { get; set; }

    public ICollection<Reference> References { get; set; } = new List<Reference>();
    public int StateId { get; set; }

    public int OrganizationId { get; set; }
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string LinkedIn { get; set; } = null!;
    public string GitHub { get; set; } = null!;
    public string City { get; set; } = null!;

    public override void Configure(EntityTypeBuilder<Persona> builder)
    {
        builder.HasKey(x => new { x.OrganizationId, x.Id });

        builder.HasOne(x => x.State)
            .WithMany(x => x.People)
            .HasForeignKey(x => x.StateId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(x => x.FirstName)
            .ConfigureColumn(StringColumnSize.VerySmall);

        builder.Property(x => x.LastName)
            .ConfigureColumn(StringColumnSize.VerySmall);

        builder.Property(x => x.Email)
            .ConfigureColumn(StringColumnSize.VerySmall);

        builder.Property(x => x.PhoneNumber)
            .ConfigureColumn(StringColumnSize.VerySmall);

        builder.Property(x => x.GitHub)
            .ConfigureColumn(StringColumnSize.VerySmall, false);

        builder.Property(x => x.LinkedIn)
            .ConfigureColumn(StringColumnSize.VerySmall, false);

        // Temporarily disable query filter for integration tests
        // builder.HasQueryFilter(x => x.IsDeleted == false);
    }
}
