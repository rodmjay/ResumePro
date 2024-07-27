#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using Microsoft.AspNetCore.Components;
using ResumePro.App.Pages.Bases;
using ResumePro.Shared.Extensions;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Pages.Resumes;

public partial class ResumeEditPage : PersonPageBase
{
    private ResumeOptions Options = new();

    [Inject] public NavigationManager NavigationManager { get; set; }

    [Parameter] public int ResumeId { get; set; }

    [Inject] public IResumeController ResumeController { get; set; }

    [Inject] public IMapper Mapper { get; set; }

    private ResumeDetails ResumeDetails { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        ResumeDetails = await ResumeController.GetResume(PersonId, ResumeId);
        Options = Mapper.Map<ResumeOptions>(ResumeDetails);
    }

    private async Task HandleValidSubmit(ResumeOptions savedResume)
    {
        var response = await ResumeController.UpdateResume(PersonId, ResumeId, savedResume);
        if (response.IsSuccessStatusCode())
        {
            var resume = response.GetObject();
            NavigationManager.NavigateTo($"/people/{PersonId}/resumes/{resume.Id}");
        }
    }

    private void HandleCancelled()
    {
        NavigationManager.NavigateTo($"/people/{PersonId}/resumes/{ResumeId}");
    }
}