using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ResumePro.Shared.Options;

public class HighlightOptions
{
    public int? Id { get; set; }

    [DisplayName("Highlight")]
    [MaxLength(512)]
    [Required]
    public string Text { get; set; } = null!;

    public int? Order { get; set; }
}