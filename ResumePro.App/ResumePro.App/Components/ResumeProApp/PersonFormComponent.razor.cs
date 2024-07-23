using Microsoft.AspNetCore.Components;
using ResumePro.App.Components.ResumeProApp.Bases;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Options;
using DropdownItem = ResumePro.Shared.Common.DropdownItem;

namespace ResumePro.App.Components.ResumeProApp
{
    public partial class PersonFormComponent : FormComponent<PersonaOptions>
    {
        [Inject]
        public IFiltersController FiltersController { get; set; }
        
        public List<DropdownItem> DropdownItems { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
            var filters = await FiltersController.GetFilters();
            DropdownItems = filters.States;
            // Use the filters to populate dropdowns or perform other initialization logic
        }
    }
}
