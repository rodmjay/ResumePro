using System.ComponentModel.DataAnnotations;

namespace ResumePro.Shared.Interfaces;

public interface IPhoneNumber
{
    [Phone] string PhoneNumber { get; set; }
}