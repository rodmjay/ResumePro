using Microsoft.AspNetCore.Components;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.App.Pages.Bases
{
    public abstract class PersonPageBase : ComponentBase
    {

        [Inject]
        public IPeopleController PeopleController { get; set; }

        [Parameter]
        public int PersonId { get; set; }

        protected PersonaDetails Person { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            Person = await PeopleController.GetPerson(PersonId);
        }
    }
}
