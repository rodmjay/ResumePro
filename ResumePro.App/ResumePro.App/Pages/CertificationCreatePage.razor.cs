#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Components;
using ResumePro.Shared.Extensions;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Options;

namespace ResumePro.App.Pages;

public partial class CertificationCreatePage
{
    [Inject] public NavigationManager NavigationManager { get; set; }

    [Inject] public ICertificationsController CertificationsController { get; set; }

    public CertificationOptions Options { get; set; } = new();

    private async Task HandleValidSubmit(CertificationOptions options)
    {
        var response = await CertificationsController.CreateCertification(PersonId, options);
        if (response.IsSuccessStatusCode()) NavigationManager.NavigateTo($"/people/{PersonId}?tab=certifications");
    }

    private void HandleCancelled()
    {
        NavigationManager.NavigateTo($"/people/{PersonId}?tab=certifications");
    }
}