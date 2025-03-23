using System.ComponentModel.DataAnnotations;

namespace ResumePro.Shared.Options;

public class ProjectOptions : IBudget, IDescription, IName, IOrder
{
    public int? Id { get; set; }
    public List<HighlightOptions> Highlights { get; set; } = new();
    public decimal? Budget { get; set; } = 0;

    [MaxLength(512)] public string Description { get; set; } = null!;

    [MaxLength(255)] [Required] public string Name { get; set; } = null!;

    public int Order { get; set; }
}