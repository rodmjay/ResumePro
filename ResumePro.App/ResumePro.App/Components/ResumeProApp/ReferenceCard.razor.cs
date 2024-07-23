using Microsoft.AspNetCore.Components;
using ResumePro.Shared.Models;

namespace ResumePro.App.Components.ResumeProApp
{
    public partial class ReferenceCard
    {
        [Parameter]
        public ReferenceDto Reference { get; set; }
    }
}
