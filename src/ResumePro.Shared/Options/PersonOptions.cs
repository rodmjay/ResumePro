using System.ComponentModel.DataAnnotations;

namespace ResumePro.Shared.Options;

public class PersonOptions : ICity, IPhoneNumber, IFirstAndLastName, IEmail, IGitHub, ILinkedIn, IStateId
{
    public List<PersonLanguageOptions> LanguageOptions { get; set; } = new();
    [Required] public string City { get; set; } = null!;

    [Required]
    [MaxLength(64)]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [MaxLength(64)] [Required] public string FirstName { get; set; } = null!;

    [MaxLength(64)] [Required] public string LastName { get; set; } = null!;

    [MaxLength(64)] [Url] public string GitHub { get; set; } = null!;

    [MaxLength(64)] [Url] public string LinkedIn { get; set; } = null!;

    [MaxLength(64)] public string PhoneNumber { get; set; } = null!;

    public int StateId { get; set; }
}