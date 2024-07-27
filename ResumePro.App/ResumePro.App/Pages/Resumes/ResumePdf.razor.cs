using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using ResumePro.App.Pages.Bases;
using ResumePro.Shared.Extensions;

namespace ResumePro.App.Pages.Resumes
{
    public partial class ResumePdf : PersonPageBase
    {
        [Inject] private AuthenticationStateProvider AuthState { get; set; }

        [Inject] private IConfiguration Config { get; set; }

        [Parameter]
        public int ResumeId { get; set; }

        public int OrganizationId { get; set; }

        private ClaimsPrincipal user;
        
        private string PdfUrl { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthState.GetAuthenticationStateAsync();

            user = authState.User;

            if (user.Identity!.IsAuthenticated)
            {
                OrganizationId = user.OrganizationId();
            }

            PdfUrl = Config["ApiBase"] +
                     $"/v1.0/people/{PersonId}/resumes/{ResumeId}/pdf?organizationId={OrganizationId}";
        }
    }
}
