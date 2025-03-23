using System.ComponentModel.DataAnnotations;

namespace ResumePro.Shared.Interfaces;

public interface ILinkedIn
{
    [Url] string LinkedIn { get; set; }
}