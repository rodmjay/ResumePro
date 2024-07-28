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

namespace ResumePro.App.Pages.References;

public partial class ReferenceCreatePage : PersonPageBase
{
    [Inject] public IReferencesController Controller { get; set; }

    public ReferenceOptions Options { get; set; } = new();

    private async Task HandleValidSubmit(ReferenceOptions options)
    {
        ActionResult<ReferenceDto> response = await Controller.CreateReference(PersonId, options);
        if (response.IsSuccessStatusCode())
        {
            ReferenceDto reference = response.GetObject();
            await EventAggregator.PublishAsync(new ReferenceCreatedEvent(reference));
            NavigationManager.NavigateTo($"/people/{PersonId}?tab=references");
        }
    }

    private void HandleCancelled()
    {
        NavigationManager.NavigateTo($"/people/{PersonId}?tab=references");
    }
}