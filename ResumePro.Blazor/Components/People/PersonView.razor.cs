#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Components;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Blazor.Components.People;

public partial class PersonView
{
    [Parameter] public int PersonId { get; set; }

    [Inject] public IPeopleController PeopleController { get; set; }

    public PersonaDetails Person { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        await LoadData();
        await base.OnParametersSetAsync();
    }

    public async Task LoadData()
    {
        Person = await PeopleController.GetPerson(PersonId);
    }
}