using Microsoft.AspNetCore.Components;
using ResumePro.App.Pages.Bases;
using ResumePro.Shared.Extensions;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Pages
{
    public partial class ResumeEditPage : PersonPageBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        
        [Parameter]
        public int ResumeId { get; set; }
        
        [Inject] public IResumeController ResumeController { get; set; }

        private readonly ResumeOptions ResumeOptions = new();

        private ResumeDetails ResumeDetails { get; set; }
        
        protected override async Task OnParametersSetAsync()
        {
            ResumeDetails = await ResumeController.GetResume(PersonId, ResumeId);
            
            ResumeOptions.Description = ResumeDetails.Description;
            ResumeOptions.JobTitle = ResumeDetails.JobTitle;
            
            await base.OnParametersSetAsync();
        }

        private async Task HandleValidSubmit(ResumeOptions savedResume)
        {
            var response = await ResumeController.UpdateResume(PersonId, ResumeId, savedResume);
            if (response.IsSuccessStatusCode())
            {
                var resume = response.GetObject();
                NavigationManager.NavigateTo($"/people/{PersonId}/resumes/{resume.Id}");
            }
        }

        private void HandleCancelled()
        {
            NavigationManager.NavigateTo($"/people/{PersonId}/resumes/{ResumeId}");
        }
    }
}
