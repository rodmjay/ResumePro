using System.ComponentModel.DataAnnotations;

namespace ResumePro.Shared.Options;

public class SchoolOptions : ILocation, IName, IEndDate
{
    public int? Id { get; set; }

    [Required] public DateTime? StartDate { get; set; }

    public List<DegreeOptions> DegreeOptions { get; set; } = new();
    public DateTime? EndDate { get; set; }

    [Required]
    public string Location { get; set; } = null!;

    [Required] public string Name { get; set; } = null!;
}