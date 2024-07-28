#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Components;
using ResumePro.App.Pages.Bases;
using ResumePro.Shared.Extensions;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Options;

namespace ResumePro.App.Pages.Jobs;

public partial class JobCreatePage : PersonPageBase
{
    private readonly JobOptions Options = new();

    [Inject] public IJobsController JobsProxy { get; set; }

    [Inject] public NavigationManager NavigationManager { get; set; }

    private async Task HandleValidSubmit(JobOptions savedJob)
    {
        var response = await JobsProxy.CreateJob(PersonId, savedJob);
        if (response.IsSuccessStatusCode())
        {
            var job = response.GetObject();
            NavigationManager.NavigateTo($"/people/{PersonId}?tab=jobs");
        }
    }

    private void HandleCancelled()
    {
        NavigationManager.NavigateTo($"/people/{PersonId}?tab=jobs");
    }
}