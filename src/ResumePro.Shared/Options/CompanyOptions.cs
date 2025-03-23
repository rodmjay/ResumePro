using System.ComponentModel.DataAnnotations;

namespace ResumePro.Shared.Options;

public class CompanyOptions
{
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    [MaxLength(255)] [Required] public string Company { get; set; } = null!;

    [MaxLength(1024)] public string Description { get; set; } = null!;

    [MaxLength(255)] public string Location { get; set; } = null!;

    public List<PositionOptions> Positions { get; set; } = new();

    public List<int> JobSkillIds { get; set; } = new();
}