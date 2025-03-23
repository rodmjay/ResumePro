using System.ComponentModel.DataAnnotations;

namespace ResumePro.Shared.Interfaces;

public interface IEmail
{
    [EmailAddress] string Email { get; set; }
}