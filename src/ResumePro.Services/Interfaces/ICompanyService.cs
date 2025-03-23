using Bespoke.Services.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Services.Interfaces;

public interface ICompanyService : IService<Company>
{
    Task<List<T>> GetCompanies<T>(int organizationId, int personId) where T : CompanyDto;
    Task<T> GetCompany<T>(int organizationId, int personId, int companyId) where T : CompanyDto;
    Task<OneOf<CompanyDetails, Result>> CreateCompany(int organizationId, int personId, CompanyOptions options);

    Task<OneOf<CompanyDetails, Result>> UpdateCompany(int organizationId, int personId, int companyId,
        CompanyOptions options);

    Task<Result> DeleteCompany(int organizationId, int personId, int companyId);
}