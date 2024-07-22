using Microsoft.AspNetCore.Components;
using ResumePro.App.Pages.Bases;
using ResumePro.Shared.Extensions;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Options;

namespace ResumePro.App.Pages
{
    public partial class SchoolCreatePage : PersonPageBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        
        [Inject]
        public ISchoolsController SchoolsController { get; set; }
        
        public SchoolOptions Options { get; set; } = new SchoolOptions();

        private async Task HandleValidSubmit(SchoolOptions options)
        {
            var response = await SchoolsController.CreateSchool(PersonId, options);
            if (response.IsSuccessStatusCode())
            {
                var job = response.GetObject();
                NavigationManager.NavigateTo($"/people/{PersonId}");
            }
        }

        private void HandleCancelled()
        {
            NavigationManager.NavigateTo($"/people/{PersonId}");
        }
    }
}
