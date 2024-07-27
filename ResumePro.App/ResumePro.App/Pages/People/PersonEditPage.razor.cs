#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using Microsoft.AspNetCore.Components;
using ResumePro.App.Pages.Bases;
using ResumePro.Shared.Extensions;
using ResumePro.Shared.Options;

namespace ResumePro.App.Pages.People;

public partial class PersonEditPage : PersonPageBase
{
    [Inject] public IMapper Mapper { get; set; }

    public PersonOptions Options { get; set; } = new();

    [Inject] public NavigationManager NavigationManager { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        Options = Mapper.Map<PersonOptions>(Person);
    }

    private async Task HandleValidSubmit(PersonOptions savedPerson)
    {
        var response = await PeopleController.UpdatePerson(PersonId, savedPerson);
        if (response.IsSuccessStatusCode()) 
            NavigationManager.NavigateTo($"/people/{PersonId}");
    }

    private void HandleCancelled()
    {
        NavigationManager.NavigateTo($"/people/{PersonId}");
    }
}