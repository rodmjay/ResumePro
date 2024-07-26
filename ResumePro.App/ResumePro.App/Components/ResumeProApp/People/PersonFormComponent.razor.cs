#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Components;
using ResumePro.App.Components.ResumeProApp.Bases;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Components.ResumeProApp.People;

public partial class PersonFormComponent : FormComponent<PersonaOptions>
{
    private FilterContainer filterContainer = new();

    [Inject] public IFiltersController FiltersController { get; set; }

    public List<StateProvinceOutput> DropdownItems { get; set; }

    protected override async Task OnInitializedAsync()
    {
        filterContainer = await FiltersController.GetFilters();
        DropdownItems = filterContainer.States;
        // Use the filters to populate dropdowns or perform other initialization logic
    }
}