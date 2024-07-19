using Microsoft.AspNetCore.Components;
using ResumePro.Shared.Models;

namespace ResumePro.App.Components.ResumeProApp
{
    public partial class JobDetailsComponent
    {
        [Parameter]
        public JobDetails JobDetails { get; set; }
    }
}
