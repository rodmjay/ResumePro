using Microsoft.AspNetCore.Components;
using ResumePro.Shared.Common;
using ResumePro.Shared.Proxies;
using ResumePro.Shared;

namespace ResumePro.App.Components.ResumeProApp
{
    public partial class PeopleList
    {
        [Inject]
        protected NavigationManager NavManager { get; set; }

        [Inject] IPeopleController PeopleController { get; set; }

        public PagedList<PersonaDto> PagedList { get; set; }

        protected PagingQuery PagingQuery { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
            await base.OnInitializedAsync();
        }

        public async Task LoadData()
        {
            this.PagedList = await PeopleController.GetPeople(null, PagingQuery);

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
