using EventAggregator.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using ResumePro.App.Components.People;
using ResumePro.Shared.Events;
using ResumePro.Shared.Extensions;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Pages.People
{
    public partial class PersonCreatePage
    {
        public PersonFormComponent Form { get; set; }

        [Inject] public IEventAggregator EventAggregator { get; set; }
        
        [Inject] public NavigationManager NavigationManager { get; set; }

        [Inject] public IPeopleController Controller { get; set; }

        protected override Task OnParametersSetAsync()
        {
            return base.OnParametersSetAsync();
        }

        public PersonOptions Options { get; set; } = new();

        private async Task HandleValidSubmit(PersonOptions options)
        {
            ActionResult<PersonaDetails> response = await Controller.CreatePerson(options);
            if (response.IsSuccessStatusCode())
            {
                PersonaDetails person = response.GetObject();
                await EventAggregator.PublishAsync(new PersonCreatedEvent(person));
                NavigationManager.NavigateTo($"/people/{person.Id}");
            }
            else
            {
                Form.HandleErrors(response.GetErrorResult());
            }
        }

        private void HandleCancelled()
        {
            NavigationManager.NavigateTo($"/");
        }
    }
}
