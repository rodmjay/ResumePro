#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using ResumePro.App.Components.Companies;
using ResumePro.App.Pages.Bases;
using ResumePro.Shared.Events;
using ResumePro.Shared.Extensions;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Pages.Jobs;

public partial class JobCreatePage : PersonPageBase
{
    public JobFormComponent Form { get; set; }
    
    private readonly CompanyOptions Options = new();

    [Inject] public ICompaniesController CompaniesProxy { get; set; }

    private async Task HandleValidSubmit(CompanyOptions savedCompany)
    {
        ActionResult<CompanyDetails> response = await CompaniesProxy.CreateCompany(PersonId, savedCompany);
        if (response.IsSuccessStatusCode())
        {
            CompanyDetails company = response.GetObject();
            await EventAggregator.PublishAsync(new JobCreatedEvent(company));
            NavigationManager.NavigateTo($"/people/{PersonId}?tab=jobs");
        }
        else
        {
            Form.HandleErrors(response.GetErrorResult());
        }
    }

    private void HandleCancelled()
    {
        NavigationManager.NavigateTo($"/people/{PersonId}?tab=jobs");
    }
}