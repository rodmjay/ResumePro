using Microsoft.AspNetCore.Components;
using ResumePro.App.Pages.Bases;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.App.Pages.Resumes
{
    public partial class ResumePdfPage : PersonPageBase
    {
        [Parameter]
        public int ResumeId { get; set; }
        [Inject]
        public IResumeController ResumeController { get; set; }
        private ResumeDetails Resume { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            Resume = await ResumeController.GetResume(PersonId, ResumeId);
        }
    }
}
