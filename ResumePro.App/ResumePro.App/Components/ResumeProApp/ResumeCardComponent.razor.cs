using Microsoft.AspNetCore.Components;
using ResumePro.Shared.Models;

namespace ResumePro.App.Components.ResumeProApp
{
    public partial class ResumeCardComponent
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Parameter]
        public int PersonId { get; set; }
        [Parameter]
        public ResumeDto Resume { get; set; }

        protected override Task OnParametersSetAsync()
        {
            return base.OnParametersSetAsync();
        }
        void GeneratePdf(int resumeId)
        {
            // Logic to generate PDF
        }

        void DeleteResume(int resumeId)
        {
            // Logic to delete the resume
        }

        void EditResume(int resumeId)
        {
            // Logic to navigate to the edit page
        }

        void OpenSettings(int resumeId)
        {
            NavigationManager.NavigateTo($"/people/{PersonId}/resumes/{resumeId}?tab=settings");
        }

        private void NavigateToResumeDetails(int resumeId)
        {
            NavigationManager.NavigateTo($"/people/{PersonId}/resumes/{resumeId}");
        }
    }
}
