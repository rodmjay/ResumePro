using Microsoft.AspNetCore.Components;
using ResumePro.Shared.Filters;

namespace ResumePro.App.Pages.People
{
    public partial class PeoplePage
    {
        public PersonaFilters Filters { get; set; } = new();
        
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        
        public async Task HandleFiltersChanged(PersonaFilters filters)
        {
            Filters = filters;
        }
        
        void CreatePerson()
        {
            NavigationManager.NavigateTo("/people/create");
        }
    }
}
