using Microsoft.AspNetCore.Components;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Components.ResumeProApp
{
    public partial class CertificationCard
    {
        [Parameter]
        public CertificationDto Certification { get; set; }
    }
}
