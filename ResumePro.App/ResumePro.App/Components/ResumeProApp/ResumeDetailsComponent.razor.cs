using Microsoft.AspNetCore.Components;
using ResumePro.Shared.Models;

namespace ResumePro.App.Components.ResumeProApp
{
    public partial class ResumeDetailsComponent
    {
        [Parameter]
        public ResumeDetails Resume { get; set; }
    }
}
