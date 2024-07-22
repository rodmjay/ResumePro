using Microsoft.AspNetCore.Components;
using ResumePro.App.Pages.Bases;
using ResumePro.Shared.Extensions;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Options;

namespace ResumePro.App.Pages
{
    public partial class JobCreatePage : PersonPageBase
    {
        [Inject]
        public IJobsController JobsProxy { get; set; }
        
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private readonly JobOptions Options = new JobOptions();

        private async Task HandleValidSubmit(JobOptions savedJob)
        {
            var response = await JobsProxy.CreateJob(PersonId, savedJob);
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
