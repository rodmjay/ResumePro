using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Components.ResumeProApp
{

    public partial class PersonSkillsComponent
    {
        Dictionary<int, bool> SkillCheckStates = new Dictionary<int, bool>();

        private Dictionary<string, Dictionary<string, int>> CategorySkills =
            new Dictionary<string, Dictionary<string, int>>();

        private Dictionary<string, int> OtherSkills = new Dictionary<string, int>();

        [Inject]
        public ISkillsController SkillsController { get; set; }

        [Inject]
        public IPersonSkillsController PersonSkillsController { get; set; }

        [Parameter]
        public int PersonId { get; set; }

        private List<SkillDto> Skills { get; set; }

        private List<PersonaSkillDto> PersonSkills { get; set; }


        async Task ToggleSelection(bool selected, int skillId)
        {
            if (selected)
            {

                var result = await PersonSkillsController.AddOrUpdateSkill(PersonId, new PersonaSkillsOptions()
                {
                    SkillId = skillId,
                });
            }
            else
            {
                var result = await PersonSkillsController.DeletePersonalSkill(PersonId, skillId);
            }

        }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            Skills = await SkillsController.GetSkills();
            PersonSkills = await PersonSkillsController.GetSkills(PersonId);

            var categories = Skills.SelectMany(x => x.Categories).Distinct()
                .ToList();

            foreach (var personSkill in PersonSkills)
            {
                SkillCheckStates.Add(personSkill.SkillId, true);
            }

            foreach (var category in categories)
            {
                CategorySkills[category] = new Dictionary<string, int>();
            }

            foreach (var skill in Skills)
            {

                SkillCheckStates.TryAdd(skill.Id, false);
                
                foreach (var category in skill.Categories)
                {
                    if (!CategorySkills[category].ContainsKey(skill.Title))
                    {
                        CategorySkills[category][skill.Title] = skill.Id;
                    }
                }
            }


            OtherSkills = Skills.Where(x => x.Categories.Count == 0)
                .ToDictionary(x => x.Title, x => x.Id);

        }
    }
}
