using Microsoft.AspNetCore.Components;
using ResumePro.Shared.Models;

namespace ResumePro.App.Components.ResumeProApp
{
    public partial class SchoolDetailsComponent
    {
        [Parameter]
        public SchoolDetails School { get; set; }
    }
}
