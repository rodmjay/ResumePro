using Microsoft.AspNetCore.Components;
using ResumePro.Shared.Common;
using ResumePro.Shared.Filters;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.App.Components.ResumeProApp
{
    public partial class PeopleList
    {

        private async Task SelectedSkillsChanged(IReadOnlyList<int> selectedValues)
        {
            this.PersonaFilters.Skills.Clear();
            this.PersonaFilters.Skills.AddRange(selectedValues);

            await LoadData();
        }

        private async Task SelectedStateChanged(int selectedValues)
        {
            this.PersonaFilters.State = selectedValues;

            await LoadData();
        }



        private async Task OnStateFilterChanged(ChangeEventArgs e)
        {
            string selectedState = e.Value.ToString();
            // Call API to filter data based on selected state
            await LoadData();
            StateHasChanged();
        }

        private FilterContainer filterContainer = new FilterContainer();

        [Inject]
        protected IFiltersController FiltersController { get; set; }

        [Inject]
        protected NavigationManager NavManager { get; set; }

        [Inject] IPeopleController PeopleController { get; set; }

        public PagedList<PersonaDto> PagedList { get; set; }

        protected PagingQuery PagingQuery { get; set; } = new();

        protected PersonaFilters PersonaFilters { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
            await base.OnInitializedAsync();
        }

        public async Task LoadData()
        {
            this.filterContainer = await FiltersController.GetFilters();
            this.PagedList = await PeopleController.GetPeople(PersonaFilters, PagingQuery);

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

            if (reload)
            {
                await LoadData();
            }

        }
    }
}
