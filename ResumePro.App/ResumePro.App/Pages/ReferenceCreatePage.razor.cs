#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Components;
using ResumePro.App.Pages.Bases;
using ResumePro.Shared.Extensions;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Options;

namespace ResumePro.App.Pages;

public partial class ReferenceCreatePage : PersonPageBase
{
    [Inject] public NavigationManager NavigationManager { get; set; }

    [Inject] public IReferencesController Controller { get; set; }

    public ReferenceOptions Options { get; set; } = new();

    private async Task HandleValidSubmit(ReferenceOptions options)
    {
        var response = await Controller.CreateReference(PersonId, options);
        if (response.IsSuccessStatusCode()) NavigationManager.NavigateTo($"/people/{PersonId}?tab=references");
    }

    private void HandleCancelled()
    {
        NavigationManager.NavigateTo($"/people/{PersonId}?tab=references");
    }
}