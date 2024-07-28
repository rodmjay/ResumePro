#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using ResumePro.App.Pages.Bases;
using ResumePro.Shared.Events;
using ResumePro.Shared.Extensions;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Pages.Certifications;

public partial class CertificationCreatePage : PersonPageBase
{
    [Inject] public ICertificationsController CertificationsController { get; set; }

    public CertificationOptions Options { get; set; } = new();

    private async Task HandleValidSubmit(CertificationOptions options)
    {
        ActionResult<CertificationDto> response = await CertificationsController.CreateCertification(PersonId, options);
        if (response.IsSuccessStatusCode())
        {
            CertificationDto certification = response.GetObject();
            await EventAggregator.PublishAsync(new CertificationCreatedEvent(certification));
            NavigationManager.NavigateTo($"/people/{PersonId}?tab=certifications");
        }
    }

    private void HandleCancelled()
    {
        NavigationManager.NavigateTo($"/people/{PersonId}?tab=certifications");
    }
}