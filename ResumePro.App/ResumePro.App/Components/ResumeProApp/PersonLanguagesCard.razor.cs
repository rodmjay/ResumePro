using Microsoft.AspNetCore.Components;
using ResumePro.Shared.Models;

namespace ResumePro.App.Components.ResumeProApp
{
    public partial class PersonLanguagesCard
    {
        [Parameter]
        public PersonaDetails Person { get; set; }
    }
}
