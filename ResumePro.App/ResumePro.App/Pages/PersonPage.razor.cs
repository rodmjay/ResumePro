using Microsoft.AspNetCore.Components;
using ResumePro.App.Pages.Bases;
using System.Web;

namespace ResumePro.App.Pages
{
    public partial class PersonPage : PersonPageBase
    {
        private string ActiveTabClass(string tabName) => tabName == currentTab ? "active" : "";
        private string ActiveTabPaneClass(string tabName) => tabName == currentTab ? "show active" : "";
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        private string currentTab;
        protected override void OnInitialized()
        {
            var uri = new Uri(NavigationManager.Uri);
            currentTab = HttpUtility.ParseQueryString(uri.Query).Get("tab") ?? "basicInfo";
        }
        void CreateNewResume()
        {
            NavigationManager.NavigateTo($"people/{PersonId}/resumes/create");
        }

        void CreateReference()
        {
            NavigationManager.NavigateTo($"people/{PersonId}/references/create");
        }


        void CreateCertification()
        {
            NavigationManager.NavigateTo($"people/{PersonId}/certifications/create");
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
        private void EditCertification(int certificationId)
        {
            NavigationManager.NavigateTo($"/people/{PersonId}/certifications/{certificationId}/edit");
        }
        private void EditSchool(int schoolId)
        {
            NavigationManager.NavigateTo($"/people/{PersonId}/schools/{schoolId}/edit");
        }

        private void EditResume(int resumeId)
        {
            NavigationManager.NavigateTo($"/people/{PersonId}/resumes/{resumeId}/edit");
        }
    }
}
