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

namespace ResumePro.App.Pages.Schools;

public partial class SchoolCreatePage : PersonPageBase
{
    [Inject] public ISchoolsController SchoolsController { get; set; }

    public SchoolOptions Options { get; set; } = new();

    private async Task HandleValidSubmit(SchoolOptions options)
    {
        ActionResult<SchoolDetails> response = await SchoolsController.CreateSchool(PersonId, options);
        if (response.IsSuccessStatusCode())
        {
            SchoolDetails school = response.GetObject();
            await EventAggregator.PublishAsync(new SchoolCreatedEvent(school));
            NavigationManager.NavigateTo($"/people/{PersonId}?tab=education");
        }
    }

    private void HandleCancelled()
    {
        NavigationManager.NavigateTo($"/people/{PersonId}?tab=education");
    }
}