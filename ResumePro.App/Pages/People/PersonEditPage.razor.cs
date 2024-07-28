#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.App.Pages.Bases;
using ResumePro.Shared.Events;
using ResumePro.Shared.Extensions;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Pages.People;

public partial class PersonEditPage : PersonPageBase
{
    public PersonOptions Options { get; set; } = new();

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        Options = Mapper.Map<PersonOptions>(Person);
    }

    private async Task HandleValidSubmit(PersonOptions savedPerson)
    {
        ActionResult<PersonaDetails> response = await PeopleController.UpdatePerson(PersonId, savedPerson);
        if (response.IsSuccessStatusCode())
        {
            var person = response.GetObject();
            await EventAggregator.PublishAsync(new PersonUpdatedEvent(person));

            NavigationManager.NavigateTo($"/people/{PersonId}");
        }
    }

    private void HandleCancelled()
    {
        NavigationManager.NavigateTo($"/people/{PersonId}");
    }
}