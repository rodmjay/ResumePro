﻿using Microsoft.AspNetCore.Components;  
using ResumePro.Shared.Filters;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.App.Components.ResumeProApp.People
{
    public partial class PersonFiltersComponent
    {
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

            FilterContainer = await FiltersController.GetFilters();

            var categories = FilterContainer.Skills.SelectMany(x => x.Categories)
                .Distinct()
                .ToList();

            foreach (var category in categories)
            {
                SkillDictionary.Add(category, new List<SkillDto>());
            }

            foreach (var skill in FilterContainer.Skills)
            {
                SkillStates.TryAdd(skill.Id, false);
                foreach (var category in skill.Categories)
                {
                    SkillDictionary[category].Add(skill);
                }
            }

            foreach (var state in FilterContainer.States)
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