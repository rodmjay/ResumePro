namespace ResumePro.Shared.Interfaces;

public interface IPosition : IStartDate, IEndDate, IId
{
    string JobTitle { get; set; }
}