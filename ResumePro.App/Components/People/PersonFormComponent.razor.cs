#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Components;
using ResumePro.App.Components.Bases;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Components.People;

public partial class PersonFormComponent : FormComponent<PersonOptions>
{
    private FilterContainer filterContainer = new();

    [Inject]
    public IFiltersController FiltersController { get; set; }

    public List<StateProvinceOutput> States { get; set; } = new();
    private List<LanguageDto> Languages { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        filterContainer = await FiltersController.GetFilters();
        States = filterContainer.States;
        Languages = filterContainer.Languages;
    }

    void AddLanguage()
    {
        Options.LanguageOptions.Add(new PersonLanguageOptions());
    }
}