using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResumePro.Domain.Entities;

public sealed class Rendering : BaseEntity<Rendering>, IRendering
{
    public Resume Resume { get; set; } = null!;
    public Template Template { get; set; } = null!;
    public int OrganizationId { get; set; }
    public int ResumeId { get; set; }
    public int TemplateId { get; set; }
    public DateTime RenderDate { get; set; }
    public string Text { get; set; } = null!;

    public override void Configure(EntityTypeBuilder<Rendering> builder)
    {
        builder.HasKey(x => new { x.OrganizationId, x.ResumeId, x.TemplateId });

        builder.Property(x => x.Text)
            .HasColumnType("TEXT")
            .IsRequired();

        builder.HasOne(x => x.Resume)
            .WithMany(x => x.Renderings)
            .HasForeignKey(x => new { x.OrganizationId, x.ResumeId })
            .HasPrincipalKey(x => new { x.OrganizationId, x.Id })
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Template)
            .WithMany(x => x.Renderings)
            .HasForeignKey(x => x.TemplateId)
            .HasPrincipalKey(x => x.Id);
    }
}