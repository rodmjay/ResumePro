#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using ResumePro.App.Components.Companies;
using ResumePro.App.Pages.Bases;
using ResumePro.Shared.Common;
using ResumePro.Shared.Events;
using ResumePro.Shared.Extensions;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Pages.Jobs;

public partial class JobEditPage : PersonPageBase
{
    public JobFormComponent Form { get; set; }

    [Inject] public ICompaniesController CompaniesController { get; set; }

    [Parameter] public int JobId { get; set; }

    public CompanyDetails Company { get; set; }

    public CompanyOptions Options { get; set; } = new();


    protected override async Task OnParametersSetAsync()
    {
        Company = await CompaniesController.GetCompany(PersonId, JobId);

        Options = Mapper.Map<CompanyOptions>(Company);

        await base.OnParametersSetAsync();
    }

    private async Task HandleDelete()
    {
        Result response = await CompaniesController.DeleteCompany(PersonId, JobId);
        if (response.Succeeded)
        {
            await EventAggregator.PublishAsync(new JobDeletedEvent());
            NavigationManager.NavigateTo($"/people/{PersonId}?tab=jobs");
        }
        else
        {
            Form.HandleErrors(response);
        }
    }

    private async Task HandleValidSubmit(CompanyOptions options)
    {
        ActionResult<CompanyDetails> response = await CompaniesController.UpdateCompany(PersonId, JobId, options);
        if (response.IsSuccessStatusCode())
        {
            CompanyDetails company = response.GetObject();
            await EventAggregator.PublishAsync(new JobUpdatedEvent(company));
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