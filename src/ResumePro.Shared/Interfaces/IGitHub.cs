using System.ComponentModel.DataAnnotations;

namespace ResumePro.Shared.Interfaces;

public interface IGitHub
{
    [Url] string GitHub { get; set; }
}