using Microsoft.AspNetCore.Components;
using ResumePro.Shared.Filters;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Blazor.Components.People
{
    public partial class PersonFiltersComponent
    {
        private void Callback()
        {
            PersonFilters.Skills.Clear();
            PersonFilters.States.Clear();

            foreach (KeyValuePair<int, bool> kvp in StateStates)
            {
                StateStates[kvp.Key] = false;
            }

            foreach (KeyValuePair<int, bool> kvp in SkillStates)
            {
                SkillStates[kvp.Key] = false;
            }
            if (FiltersChanged.HasDelegate)
            {
                FiltersChanged.InvokeAsync(PersonFilters);

            }
        }
        private Dictionary<int, bool> StateStates { get; set; } = new();
        private Dictionary<int, bool> SkillStates { get; set; } = new();
        
        private Dictionary<string, List<SkillDto>> SkillDictionary = new();
        
        [Inject]
        public IFiltersController FiltersController { get; set; }
        [Parameter] public PersonaFilters PersonFilters { get; set; }

        [Parameter]
        public EventCallback<PersonaFilters> FiltersChanged { get; set; }
        
        public FilterContainer FilterContainer { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            
            SkillDictionary.Clear();

            FilterContainer = await FiltersController.GetFilters();

            List<string> categories = FilterContainer.Skills.SelectMany(x => x.Categories)
                .Distinct()
                .ToList();

            foreach (string category in categories)
            {
                SkillDictionary.TryAdd(category, new List<SkillDto>());
            }

            foreach (SkillDto skill in FilterContainer.Skills)
            {
                SkillStates.TryAdd(skill.Id, false);
                foreach (string category in skill.Categories)
                {
                    if (SkillDictionary.ContainsKey(category))
                    {
                        SkillDictionary[category].Add(skill);

                    }
                    else
                    {
                        SkillDictionary.Add(category, [skill]);
                    }
                }
            }

            foreach (StateProvinceOutput state in FilterContainer.States)
            {
                StateStates.TryAdd(state.Id, false);
            }
        }
        void ToggleSkill(int skillId)
        {
            if (PersonFilters.Skills.Contains(skillId))
            {
                PersonFilters.Skills.Remove(skillId);
                SkillStates[skillId] = false;

            }
            else
            {
                PersonFilters.Skills.Add(skillId);
                SkillStates[skillId] = true;

            }

            if (FiltersChanged.HasDelegate)
            {
                FiltersChanged.InvokeAsync(PersonFilters);
                
            }
        }
        void ToggleState(int stateId)
        {
            
            
            if (PersonFilters.States.Contains(stateId))
            {
                PersonFilters.States.Remove(stateId);
                StateStates[stateId] = false;
            }
            else
            {
                PersonFilters.States.Add(stateId);
                StateStates[stateId] = true;
            }

            if (FiltersChanged.HasDelegate)
            {
                FiltersChanged.InvokeAsync(PersonFilters);
            }
        }
    }
}
