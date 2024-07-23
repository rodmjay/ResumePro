using Microsoft.AspNetCore.Components;
using ResumePro.App.Pages.Bases;
using System.Web;
using AutoMapper;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Pages
{
    public partial class PersonPage : PersonPageBase
    {
        [Inject]
        public IMapper Mapper { get; set; }
        
        [Inject]
        public IReferencesController ReferencesController { get; set; }
        private async Task ItemDropped(DraggableDroppedEventArgs<ReferenceDto> dropItem)
        {
            dropItem.Item.Order = dropItem.IndexInZone + 1;

            var referenceOptions = Mapper.Map<ReferenceOptions>(dropItem.Item);
            var result = await ReferencesController.UpdateReference(PersonId, dropItem.Item.Id, referenceOptions);
            
            
        }
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
        private void EditReference(int referenceId)
        {
            NavigationManager.NavigateTo($"/people/{PersonId}/references/{referenceId}/edit");
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
