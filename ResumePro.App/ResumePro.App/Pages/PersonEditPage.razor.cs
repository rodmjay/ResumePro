using Microsoft.AspNetCore.Components;
using ResumePro.App.Pages.Bases;
using ResumePro.Shared.Extensions;
using ResumePro.Shared.Options;

namespace ResumePro.App.Pages
{
    public partial class PersonEditPage : PersonPageBase
    {
        List<string> errorMessages = new List<string>();


        public PersonaOptions Options { get; set; } = new();

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            Options.FirstName = Person.FirstName;
            Options.LastName = Person.LastName;
            Options.GitHub = Person.GitHub;
            Options.LinkedIn = Person.LinkedIn;
            Options.StateId = Person.StateId;
            Options.City = Person.City;
            Options.Email = Person.Email;
        }

        private async Task HandleValidSubmit(PersonaOptions savedPerson)
        {
            errorMessages.Clear();  // Clear previous errors

            var response = await PeopleController.UpdatePerson(PersonId, savedPerson);
            if (response.IsSuccessStatusCode())
            {
                var job = response.GetObject();
                NavigationManager.NavigateTo($"/people/{PersonId}");
            }
            else
            {
                var errorContent = response.GetResult().Errors.Select(x=>x.Description);
                errorMessages.AddRange(errorContent);
            }
            
            StateHasChanged();
        }

        private void HandleCancelled()
        {
            NavigationManager.NavigateTo($"/people/{PersonId}");
        }
    }
}
