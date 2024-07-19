using Microsoft.AspNetCore.Components;
using ResumePro.Shared.Models;

namespace ResumePro.App.Components.ResumeProApp
{
    public partial class PersonContactInfoComponent
    {
        [Parameter]
        public PersonaDto Person { get; set; }
    }
}
