using System.ComponentModel.DataAnnotations;

namespace ResumePro.Shared.Options;

public class CertificationOptions : IName, IBody
{
    [Required] public DateTime? Date { get; set; }

    [MaxLength(255)] [Required] public string Body { get; set; } = null!;

    [Required] [MaxLength(255)] public string Name { get; set; } = null!;
}