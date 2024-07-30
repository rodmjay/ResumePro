#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using ResumePro.App.Pages.Bases;
using ResumePro.Blazor.Components.Schools;
using ResumePro.Shared.Common;
using ResumePro.Shared.Events;
using ResumePro.Shared.Extensions;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Pages.Schools;

public partial class SchoolEditPage : PersonPageBase
{
    private SchoolFormComponent Form { get; set; }

    [Inject] public ISchoolsController SchoolsController { get; set; }

    [Parameter] public int SchoolId { get; set; }

    public SchoolDetails School { get; set; }

    public SchoolOptions Options { get; set; } = new();

    protected override async Task OnParametersSetAsync()
    {
        School = await SchoolsController.GetSchool(PersonId, SchoolId);
        Options = Mapper.Map<SchoolOptions>(School);
        await base.OnParametersSetAsync();
    }

    private async Task HandleDelete()
    {
        Result result = await SchoolsController.DeleteSchool(PersonId, SchoolId);
        if (result.Succeeded)
        {
            await EventAggregator.PublishAsync(new SchoolDeletedEvent());
            NavigationManager.NavigateTo($"/people/{PersonId}?tab=education");
        }
        else
        {
            Form.HandleErrors(result);
        }
    }

    private async Task HandleValidSubmit(SchoolOptions options)
    {
        ActionResult<SchoolDetails> response = await SchoolsController.UpdateSchool(PersonId, SchoolId, options);
        if (response.IsSuccessStatusCode())
        {
            await EventAggregator.PublishAsync(new SchoolUpdatedEvent(response.GetObject()));
            NavigationManager.NavigateTo($"/people/{PersonId}?tab=education");
        }
        else
        {
            Form.HandleErrors(response.GetErrorResult());
        }
    }

    private void HandleCancelled()
    {
        NavigationManager.NavigateTo($"/people/{PersonId}?tab=education");
    }
}