#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Web;
using Microsoft.AspNetCore.Components;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.App.Pages;

public partial class ResumePage
{
    private string currentTab;
    [Inject] private IResumeController ResumeController { get; set; }

    [Inject] public NavigationManager NavigationManager { get; set; }

    [Parameter] public int ResumeId { get; set; }

    [Parameter] public int PersonId { get; set; }

    private ResumeDetails ResumeDetails { get; set; }

    private async Task Regenerate()
    {
        ResumeDetails = await ResumeController.Generate(PersonId, ResumeId);
    }

    protected override void OnInitialized()
    {
        var uri = new Uri(NavigationManager.Uri);
        currentTab = HttpUtility.ParseQueryString(uri.Query).Get("tab") ?? "home";
    }

    protected override async Task OnParametersSetAsync()
    {
        ResumeDetails = await ResumeController.GetResume(PersonId, ResumeId);
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
        var result = await ResumeController.DeleteResume(PersonId, ResumeId);
        if (result.Succeeded)
        {
            NavigationManager.NavigateTo($"/people/{PersonId}/?tab=resumes");
        }
    }
}