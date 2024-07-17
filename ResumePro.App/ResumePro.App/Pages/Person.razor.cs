using Microsoft.AspNetCore.Components;
using ResumePro.Shared;
using ResumePro.Shared.Interfaces;

namespace ResumePro.App.Pages
{
    public partial class Person
    {
        [Parameter]
        public int PersonId { get; set; }

        [Inject]
        public IPeopleController PeopleController { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public PersonaDetails Persona { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            await LoadData();
            await base.OnParametersSetAsync();
        }

        public async Task LoadData()
        {
            Persona = await PeopleController.GetPerson(PersonId);
        }

        private void NavigateToResumeDetails(int resumeId)
        {
            NavigationManager.NavigateTo($"/people/{PersonId}/resumes/{resumeId}");
        }
    }
}
