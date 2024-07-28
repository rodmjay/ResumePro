#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using Microsoft.AspNetCore.Components;
using ResumePro.App.Pages.Bases;
using ResumePro.Shared.Extensions;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Options;

namespace ResumePro.App.Pages.References;

public partial class ReferenceEditPage : PersonPageBase
{
    [Inject] public IMapper Mapper { get; set; }

    [Parameter] public int ReferenceId { get; set; }

    public ReferenceOptions Options { get; set; }

    [Inject] public NavigationManager NavigationManager { get; set; }

    [Inject] public IReferencesController ReferencesController { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        var reference = await ReferencesController.Get(PersonId, ReferenceId);

        Options = Mapper.Map<ReferenceOptions>(reference);
    }

    private async Task HandleValidSubmit(ReferenceOptions options)
    {
        var response = await ReferencesController.UpdateReference(PersonId, ReferenceId, options);
        if (response.IsSuccessStatusCode())
        {
            NavigationManager.NavigateTo($"/people/{PersonId}?tab=references");
        }
    }

    private void HandleCancelled()
    {
        NavigationManager.NavigateTo($"/people/{PersonId}?tab=references");
    }

    private async Task HandleDelete()
    {
        var result = await ReferencesController.DeleteReference(PersonId, ReferenceId);
        if (result.Succeeded) NavigationManager.NavigateTo($"/people/{PersonId}?tab=references");
    }
}