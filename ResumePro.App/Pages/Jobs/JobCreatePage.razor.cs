#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using ResumePro.App.Pages.Bases;
using ResumePro.Blazor.Components.Jobs;
using ResumePro.Shared.Events;
using ResumePro.Shared.Extensions;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Pages.Jobs;

public partial class JobCreatePage : PersonPageBase
{
    public JobFormComponent Form { get; set; }
    
    private readonly JobOptions Options = new();

    [Inject] public IJobsController JobsProxy { get; set; }

    private async Task HandleValidSubmit(JobOptions savedJob)
    {
        ActionResult<JobDetails> response = await JobsProxy.CreateJob(PersonId, savedJob);
        if (response.IsSuccessStatusCode())
        {
            JobDetails job = response.GetObject();
            await EventAggregator.PublishAsync(new JobCreatedEvent(job));
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