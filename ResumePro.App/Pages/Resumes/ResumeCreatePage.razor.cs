#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using EventAggregator.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using ResumePro.App.Pages.Bases;
using ResumePro.Shared.Events;
using ResumePro.Shared.Extensions;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Pages.Resumes;

public partial class ResumeCreatePage : PersonPageBase
{
    private readonly ResumeOptions ResumeOptions = new();

    [Inject] public IResumeController ResumeController { get; set; }

    private async Task HandleValidSubmit(ResumeOptions savedResume)
    {
        ActionResult<ResumeDetails> response = await ResumeController.CreateResume(PersonId, savedResume);
        if (response.IsSuccessStatusCode())
        {
            ResumeDetails resume = response.GetObject();

            await EventAggregator.PublishAsync(new ResumeCreatedEvent(resume));

            NavigationManager.NavigateTo($"/people/{PersonId}/resumes/{resume.Id}");
        }
    }

    private void HandleCancelled()
    {
        NavigationManager.NavigateTo($"/people/{PersonId}?tab=resumes");
    }

}