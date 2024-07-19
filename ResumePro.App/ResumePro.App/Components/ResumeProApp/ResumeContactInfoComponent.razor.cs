using Microsoft.AspNetCore.Components;
using ResumePro.Shared.Models;

namespace ResumePro.App.Components.ResumeProApp
{
    public partial class ResumeContactInfoComponent
    {
        [Parameter]
        public ResumeDto Resume { get; set; }
    }
}
