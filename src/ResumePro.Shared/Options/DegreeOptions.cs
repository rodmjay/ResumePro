using System.ComponentModel.DataAnnotations;

namespace ResumePro.Shared.Options;

public class DegreeOptions : IName, IOrder
{
    public int? Id { get; set; }

    [MaxLength(255)] [Required] public string Name { get; set; } = null!;

    public int Order { get; set; }
}