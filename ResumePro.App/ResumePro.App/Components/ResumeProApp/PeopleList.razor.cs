#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Components;
using ResumePro.Shared.Common;
using ResumePro.Shared.Filters;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.App.Components.ResumeProApp;

public partial class PeopleList
{
    private FilterContainer filterContainer = new();

    [Inject] protected IFiltersController FiltersController { get; set; }

    [Inject] protected NavigationManager NavManager { get; set; }

    [Inject] private IPeopleController PeopleController { get; set; }

    public PagedList<PersonaDto> PagedList { get; set; }

    protected PagingQuery PagingQuery { get; set; } = new();

    protected PersonaFilters PersonaFilters { get; set; } = new();

    private async Task SelectedSkillsChanged(IReadOnlyList<int> selectedValues)
    {
        PersonaFilters.Skills.Clear();
        PersonaFilters.Skills.AddRange(selectedValues);

        await LoadData();
    }

    private async Task SelectedStateChanged(int selectedValues)
    {
        PersonaFilters.State = selectedValues;

        await LoadData();
    }


    private async Task OnStateFilterChanged(ChangeEventArgs e)
    {
        var selectedState = e.Value.ToString();
        // Call API to filter data based on selected state
        await LoadData();
        StateHasChanged();
    }

    protected override async Task OnInitializedAsync()
    {
        await LoadData();
        await base.OnInitializedAsync();
    }

    public async Task LoadData()
    {
        filterContainer = await FiltersController.GetFilters();
        PagedList = await PeopleController.GetPeople(PersonaFilters, PagingQuery);
    }

    private void HandleRowSelected(DataGridRowMouseEventArgs<PersonaDto> evnt)
    {
        NavManager.NavigateTo($"/people/{evnt.Item.Id}");
    }

    private async Task HandlePageChanged(DataGridPageChangedEventArgs args)
    {
        var reload = false;

        if (PagedList.CurrentPage != args.Page)
        {
            reload = true;
            PagingQuery.Page = args.Page;
        }

        if (PagedList.PageSize != args.PageSize)
        {
            reload = true;
            PagingQuery.Size = args.PageSize;
        }

        if (reload) await LoadData();
    }
}