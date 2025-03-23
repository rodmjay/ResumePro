namespace ResumePro.Shared.Interfaces;

public interface ICompany : IStartDate, IEndDate, IId
{
    string CompanyName { get; set; }
    string Location { get; set; }
    string Description { get; set; }
}