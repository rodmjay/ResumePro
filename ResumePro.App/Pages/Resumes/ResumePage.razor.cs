#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Runtime.InteropServices;
using System.Web;
using Microsoft.AspNetCore.Components;
using ResumePro.App.Pages.Bases;
using ResumePro.Shared.Common;
using ResumePro.Shared.Events;
using ResumePro.Shared.Extensions;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Pages.Resumes;

public partial class ResumePage : PersonPageBase
{
    private bool ShowModal = false;
    private string currentTab;
    [Inject] private IResumeController ResumeController { get; set; }

    [Inject] private IResumeSettingsController ResumeSettings { get; set; }

    [Parameter] public int ResumeId { get; set; }

    private ResumeDetails ResumeDetails { get; set; }
    private ResumeSettingsOptions Options { get; set; }

    private async Task Regenerate()
    {
        // ResumeDetails = await ResumeController.Generate(PersonId, ResumeId);
    }
    private async Task UpdateSettings(ResumeSettingsOptions options)
    {
        var response = await ResumeSettings.UpdateSettings(PersonId, ResumeId, options);
        if (response.IsSuccessStatusCode())
        {
            var settings = response.GetObject();
            await EventAggregator.PublishAsync(new ResumeSettingsUpdatedEvent(settings));

            currentTab = "home";
            NavigationManager.NavigateTo($"people/{PersonId}/resumes/{ResumeId}?tab={currentTab}");
        }
    }
    private void HandleCancelled()
    {
        currentTab = "home";
        NavigationManager.NavigateTo($"people/{PersonId}/resumes/{ResumeId}?tab={currentTab}");
    }
    protected override void OnInitialized()
    {
        Uri uri = new Uri(NavigationManager.Uri);
        currentTab = HttpUtility.ParseQueryString(uri.Query).Get("tab") ?? "home";
    }

    protected override async Task OnParametersSetAsync()
    {
        ResumeDetails = await ResumeController.GetResume(PersonId, ResumeId);
        if (ResumeDetails != null)
        {
            Options = Mapper.Map<ResumeSettingsOptions>(ResumeDetails.Settings);
        }
    }

    void ViewResume()
    {
        NavigationManager.NavigateTo($"/people/{PersonId}/resumes/{ResumeId}/pdf");
    }

    private string ActiveTabClass(string tabName)
    {
        return tabName == currentTab ? "active" : "";
    }

    private string ActiveTabPaneClass(string tabName)
    {
        return tabName == currentTab ? "show active" : "";
    }


    private void EditResume()
    {
        NavigationManager.NavigateTo($"/people/{PersonId}/resumes/{ResumeId}/edit");
    }


    private async Task DeleteResume()
    {
        Result result = await ResumeController.DeleteResume(PersonId, ResumeId);
        if (result.Succeeded)
        {
            await EventAggregator.PublishAsync(new ResumeDeletedEvent());

            NavigationManager.NavigateTo($"/people/{PersonId}/?tab=resumes");
        }
    }
}