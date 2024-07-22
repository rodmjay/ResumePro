using Microsoft.AspNetCore.Components;
using ResumePro.App.Pages.Bases;

namespace ResumePro.App.Pages
{
    public partial class PersonPage : PersonPageBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        void CreateNewResume()
        {
            NavigationManager.NavigateTo($"people/{PersonId}/resumes/create");
        }

        void EditPersonDetails()
        {
            NavigationManager.NavigateTo($"people/{PersonId}/edit");
        }

        void CreateNewJob()
        {
            NavigationManager.NavigateTo($"people/{PersonId}/jobs/create");
        }

        void CreateSchool()
        {
            NavigationManager.NavigateTo($"people/{PersonId}/schools/create");
        }

        private void EditJob(int jobId)
        {
            NavigationManager.NavigateTo($"/people/{PersonId}/jobs/{jobId}/edit");
        }

        private void EditResume(int resumeId)
        {
            NavigationManager.NavigateTo($"/people/{PersonId}/resumes/{resumeId}/edit");
        }
    }
}
