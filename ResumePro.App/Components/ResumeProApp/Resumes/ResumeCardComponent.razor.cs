#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Components;
using ResumePro.Shared.Models;

namespace ResumePro.App.Components.ResumeProApp.Resumes;

public partial class ResumeCardComponent
{
    [Inject] public NavigationManager NavigationManager { get; set; }

    [Parameter] public int PersonId { get; set; }

    [Parameter] public ResumeDto Resume { get; set; }

    protected override Task OnParametersSetAsync()
    {
        return base.OnParametersSetAsync();
    }

    private void GeneratePdf(int resumeId)
    {
        // Logic to generate PDF
        NavigationManager.NavigateTo($"/people/{PersonId}/resumes/{resumeId}/pdf");
    }

    private void DeleteResume(int resumeId)
    {
        // Logic to delete the resume
    }

    private void EditResume(int resumeId)
    {
        NavigationManager.NavigateTo($"/people/{PersonId}/resumes/{resumeId}/edit");

    }

    private void OpenSettings(int resumeId)
    {
        NavigationManager.NavigateTo($"/people/{PersonId}/resumes/{resumeId}?tab=settings");
    }

    private void NavigateToResumeDetails(int resumeId)
    {
        NavigationManager.NavigateTo($"/people/{PersonId}/resumes/{resumeId}");
    }
}