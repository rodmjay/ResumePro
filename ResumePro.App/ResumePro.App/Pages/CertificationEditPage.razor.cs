using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using ResumePro.App.Pages.Bases;
using ResumePro.Shared.Extensions;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Options;

namespace ResumePro.App.Pages
{
    public partial class CertificationEditPage : PersonPageBase
    {
        [Inject]
        public IMapper Mapper { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public int CertificationId { get; set; }

        [Inject]
        public ICertificationsController CertificationsController { get; set; }

        public CertificationOptions Options { get; set; } = new CertificationOptions();

        protected override async Task OnParametersSetAsync()
        {
            var certification = await CertificationsController.Get(PersonId, CertificationId);

            Options = Mapper.Map<CertificationOptions>(certification);
            
            await base.OnParametersSetAsync();
        }

        private async Task HandleValidSubmit(CertificationOptions options)
        {
            var response = await CertificationsController.Update(PersonId, CertificationId, options);
            if (response.IsSuccessStatusCode())
            {
                NavigationManager.NavigateTo($"/people/{PersonId}?tab=certifications");
            }
        }

        private async Task HandleDelete()
        {
            var response = await CertificationsController.Delete(PersonId, CertificationId);
            if (response.Succeeded)
            {
                NavigationManager.NavigateTo($"/people/{PersonId}?tab=certifications");

            }
        }

        private void HandleCancelled()
        {
            NavigationManager.NavigateTo($"/people/{PersonId}?tab=certifications");
        }
    }
}
