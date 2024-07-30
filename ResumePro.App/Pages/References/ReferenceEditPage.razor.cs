#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using ResumePro.App.Pages.Bases;
using ResumePro.Blazor.Components.References;
using ResumePro.Shared.Common;
using ResumePro.Shared.Events;
using ResumePro.Shared.Extensions;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Pages.References;

public partial class ReferenceEditPage : PersonPageBase
{
    public ReferenceFormComponent Form { get; set; }

    [Parameter] public int ReferenceId { get; set; }

    public ReferenceOptions Options { get; set; }

    [Inject] public IReferencesController ReferencesController { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        ReferenceDto reference = await ReferencesController.Get(PersonId, ReferenceId);

        Options = Mapper.Map<ReferenceOptions>(reference);
    }

    private async Task HandleValidSubmit(ReferenceOptions options)
    {
        ActionResult<ReferenceDto> response = await ReferencesController.UpdateReference(PersonId, ReferenceId, options);
        if (response.IsSuccessStatusCode())
        {
            ReferenceDto reference = response.GetObject();
            await EventAggregator.PublishAsync(new ReferenceUpdatedEvent(reference));

            NavigationManager.NavigateTo($"/people/{PersonId}?tab=references");
        }
        else
        {
            Form.HandleErrors(response.GetErrorResult());
        }
    }

    private void HandleCancelled()
    {
        NavigationManager.NavigateTo($"/people/{PersonId}?tab=references");
    }

    private async Task HandleDelete()
    {
        Result result = await ReferencesController.DeleteReference(PersonId, ReferenceId);
        if (result.Succeeded)
        {
            await EventAggregator.PublishAsync(new ReferenceDeletedEvent());
            NavigationManager.NavigateTo($"/people/{PersonId}?tab=references");
        }
        else
        {
            Form.HandleErrors(result);
        }
    }
}