#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Components;
using ResumePro.Shared.Common;
using ResumePro.Shared.Filters;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.App.Components.People;

public partial class PeopleListComponent
{
    [Inject] protected NavigationManager NavManager { get; set; }

    [Inject] private IPeopleController PeopleController { get; set; }

    public PagedList<PersonaDto> PagedList { get; set; }

    protected PagingQuery PagingQuery { get; set; } = new();

    [Parameter]
    public PersonaFilters PersonFilters { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        await LoadData();
    }

    public async Task LoadData()
    {
        PagedList = await PeopleController.GetPeople(PersonFilters, PagingQuery);
    }

    private void HandleRowSelected(DataGridRowMouseEventArgs<PersonaDto> evnt)
    {
        NavManager.NavigateTo($"/people/{evnt.Item.Id}");
    }

    private async Task HandlePageChanged(DataGridPageChangedEventArgs args)
    {
        bool reload = false;

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