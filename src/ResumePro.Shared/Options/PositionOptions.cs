using System.ComponentModel.DataAnnotations;

namespace ResumePro.Shared.Options;

public class PositionOptions : IJobTitle, IStartDate, IEndDate, IId, IValidatableObject
{
    public List<HighlightOptions> Highlights { get; set; } = new();
    public List<ProjectOptions> Projects { get; set; } = new();
    public DateTime? EndDate { get; set; }
    public int Id { get; set; }

    [Required] public string JobTitle { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (StartDate > DateTime.Now)
            yield return new ValidationResult("Start date cannot be in the future.", new[] { nameof(StartDate) });

        if (EndDate.HasValue && EndDate < StartDate)
            yield return new ValidationResult("End date must be on or after the start date.",
                new[] { nameof(EndDate) });
    }
}