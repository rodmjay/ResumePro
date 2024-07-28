#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Components;
using ResumePro.App.Pages.Bases;
using ResumePro.Shared.Extensions;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Options;

namespace ResumePro.App.Pages.Schools;

public partial class SchoolCreatePage : PersonPageBase
{
    [Inject] public NavigationManager NavigationManager { get; set; }

    [Inject] public ISchoolsController SchoolsController { get; set; }

    public SchoolOptions Options { get; set; } = new();

    private async Task HandleValidSubmit(SchoolOptions options)
    {
        var response = await SchoolsController.CreateSchool(PersonId, options);
        if (response.IsSuccessStatusCode())
        {
            var job = response.GetObject();
            NavigationManager.NavigateTo($"/people/{PersonId}?tab=education");
        }
    }

    private void HandleCancelled()
    {
        NavigationManager.NavigateTo($"/people/{PersonId}?tab=education");
    }
}