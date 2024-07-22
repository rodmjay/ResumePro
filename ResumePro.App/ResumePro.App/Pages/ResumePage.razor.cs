using Microsoft.AspNetCore.Components;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using System.Web;

namespace ResumePro.App.Pages
{
    public partial class ResumePage
    {
        [Inject] private IResumeController ResumeController { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Parameter]
        public int ResumeId { get; set; }

        [Parameter]
        public int PersonId { get; set; }

        private ResumeDetails ResumeDetails { get; set; }

        private string currentTab;

        private async Task Regenerate()
        {
            ResumeDetails = await ResumeController.Generate(PersonId, ResumeId);
        }

        protected override void OnInitialized()
        {
            var uri = new Uri(NavigationManager.Uri);
            currentTab = HttpUtility.ParseQueryString(uri.Query).Get("tab") ?? "home";
        }

        protected override async Task OnParametersSetAsync()
        {
            ResumeDetails = await ResumeController.GetResume(PersonId, ResumeId);
        }


        private string ActiveTabClass(string tabName) => tabName == currentTab ? "active" : "";
        private string ActiveTabPaneClass(string tabName) => tabName == currentTab ? "show active" : "";
    }
}
