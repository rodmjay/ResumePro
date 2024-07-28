#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using ResumePro.App.Pages.Bases;
using ResumePro.Shared.Common;
using ResumePro.Shared.Events;
using ResumePro.Shared.Extensions;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Pages.Certifications;

public partial class CertificationEditPage : PersonPageBase
{
    [Parameter] public int CertificationId { get; set; }

    [Inject] public ICertificationsController CertificationsController { get; set; }

    public CertificationOptions Options { get; set; } = new();

    protected override async Task OnParametersSetAsync()
    {
        CertificationDto certification = await CertificationsController.Get(PersonId, CertificationId);

        Options = Mapper.Map<CertificationOptions>(certification);

        await base.OnParametersSetAsync();
    }

    private async Task HandleValidSubmit(CertificationOptions options)
    {
        ActionResult<CertificationDto> response = await CertificationsController.Update(PersonId, CertificationId, options);
        if (response.IsSuccessStatusCode())
        {
            CertificationDto certification = response.GetObject();
            await EventAggregator.PublishAsync(new CertificationUpdatedEvent(certification));
            NavigationManager.NavigateTo($"/people/{PersonId}?tab=certifications");
        }
    }

    private async Task HandleDelete()
    {
        Result response = await CertificationsController.Delete(PersonId, CertificationId);
        if (response.Succeeded)
        {
            await EventAggregator.PublishAsync(new CertificationDeletedEvent());
            NavigationManager.NavigateTo($"/people/{PersonId}?tab=certifications");
        }
    }

    private void HandleCancelled()
    {
        NavigationManager.NavigateTo($"/people/{PersonId}?tab=certifications");
    }
}