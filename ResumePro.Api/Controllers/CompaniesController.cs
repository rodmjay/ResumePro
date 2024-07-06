#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;

namespace ResumePro.Api.Controllers;

[Route("v1.0/companies")]
public class CompaniesController : BaseController
{
    public CompaniesController(IServiceProvider serviceProvider, ICompanyService companyService) : base(serviceProvider)
    {
    }
}