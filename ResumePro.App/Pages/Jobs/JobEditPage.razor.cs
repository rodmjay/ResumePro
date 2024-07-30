#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using ResumePro.App.Components.Jobs;
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

    [Inject] public IJobsController JobsController { get; set; }

    [Parameter] public int JobId { get; set; }

    public JobDetails Job { get; set; }

    public JobOptions Options { get; set; } = new();


    protected override async Task OnParametersSetAsync()
    {
        Job = await JobsController.GetJob(PersonId, JobId);

        Options = Mapper.Map<JobOptions>(Job);

        await base.OnParametersSetAsync();
    }

    private async Task HandleDelete()
    {
        Result response = await JobsController.DeleteJob(PersonId, JobId);
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

    private async Task HandleValidSubmit(JobOptions options)
    {
        ActionResult<JobDetails> response = await JobsController.UpdateJob(PersonId, JobId, options);
        if (response.IsSuccessStatusCode())
        {
            JobDetails job = response.GetObject();
            await EventAggregator.PublishAsync(new JobUpdatedEvent(job));
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