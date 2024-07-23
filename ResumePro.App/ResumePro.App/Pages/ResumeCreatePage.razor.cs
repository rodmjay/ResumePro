#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Components;
using ResumePro.App.Pages.Bases;
using ResumePro.Shared.Extensions;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Options;

namespace ResumePro.App.Pages;

public partial class ResumeCreatePage : PersonPageBase
{
    private readonly ResumeOptions ResumeOptions = new();

    [Inject] public NavigationManager Navigation { get; set; }

    [Inject] public IResumeController ResumeController { get; set; }

    private async Task HandleValidSubmit(ResumeOptions savedResume)
    {
        var response = await ResumeController.CreateResume(PersonId, savedResume);
        if (response.IsSuccessStatusCode())
        {
            var resume = response.GetObject();
            Navigation.NavigateTo($"/people/{PersonId}/resumes/{resume.Id}");
        }
    }

    private void HandleCancelled()
    {
        Navigation.NavigateTo($"/people/{PersonId}?tab=resumes");
    }
}