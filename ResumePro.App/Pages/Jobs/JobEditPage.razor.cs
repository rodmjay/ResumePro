﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using Microsoft.AspNetCore.Components;
using ResumePro.App.Pages.Bases;
using ResumePro.Shared.Extensions;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Pages.Jobs;

public partial class JobEditPage : PersonPageBase
{
    [Inject] public IMapper Mapper { get; set; }

    [Inject] public NavigationManager NavigationManager { get; set; }

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
        var response = await JobsController.DeleteJob(PersonId, JobId);
        if (response.Succeeded) NavigationManager.NavigateTo($"/people/{PersonId}?tab=jobs");
    }

    private async Task HandleValidSubmit(JobOptions options)
    {
        var response = await JobsController.UpdateJob(PersonId, JobId, options);
        if (response.IsSuccessStatusCode()) NavigationManager.NavigateTo($"/people/{PersonId}?tab=jobs");
    }

    private void HandleCancelled()
    {
        NavigationManager.NavigateTo($"/people/{PersonId}?tab=jobs");
    }
}