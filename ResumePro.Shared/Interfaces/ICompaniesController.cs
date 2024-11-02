#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Interfaces;

public interface ICompaniesController
{
    Task<List<CompanyDetails>> GetCompanies(int personId);
    Task<CompanyDetails> GetCompany(int personId, int companyId);
    Task<ActionResult<CompanyDetails>> CreateCompany(int personId, CompanyOptions options);

    Task<ActionResult<CompanyDetails>> UpdateCompany(int personId, int companyId,
        CompanyOptions options);

    Task<Result> DeleteCompany(int personId, int jobId);
}