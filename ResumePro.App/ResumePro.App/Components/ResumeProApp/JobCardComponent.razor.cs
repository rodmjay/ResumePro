using Microsoft.AspNetCore.Components;
using ResumePro.Shared.Models;

namespace ResumePro.App.Components.ResumeProApp
{
    public partial class JobCardComponent
    {
        [Parameter]
        public JobDetails Job { get; set; }
    }
}
