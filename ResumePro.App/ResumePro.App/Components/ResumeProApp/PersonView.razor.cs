using Microsoft.AspNetCore.Components;
using ResumePro.Shared;
using ResumePro.Shared.Interfaces;

namespace ResumePro.App.Components.ResumeProApp
{
    public partial class PersonView
    {
        [Parameter]
        public int PersonId { get; set; }

        [Inject]
        public IPeopleController PeopleController { get; set; }

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
}
