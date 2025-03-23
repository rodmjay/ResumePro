namespace ResumePro.Api.Interfaces;

public interface ICompaniesController
{
    Task<List<CompanyDetails>> GetCompanies(int personId);
    Task<CompanyDetails> GetCompany(int personId, int companyId);
    Task<ActionResult<CompanyDetails>> CreateCompany(int personId, CompanyOptions options);

    Task<ActionResult<CompanyDetails>> UpdateCompany(int personId, int companyId,
        CompanyOptions options);

    Task<Result> DeleteCompany(int personId, int jobId);
}