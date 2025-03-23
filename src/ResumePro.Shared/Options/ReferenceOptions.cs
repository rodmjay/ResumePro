using System.ComponentModel.DataAnnotations;

namespace ResumePro.Shared.Options;

public class ReferenceOptions : IText, IName, IOrder
{
    [MaxLength(255)] [Required] public string Name { get; set; } = null!;

    public int Order { get; set; }

    [MaxLength(1024)] [Required] public string Text { get; set; } = null!;
}