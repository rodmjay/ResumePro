using Microsoft.AspNetCore.Components;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.App.Components.People
{

    public partial class PersonSkillsComponent
    {
        Dictionary<int, bool> SkillCheckStates = new();

        private Dictionary<string, Dictionary<string, int>> CategorySkills = new();

        private Dictionary<string, int> OtherSkills = new();

        [Inject]
        public ISkillsController SkillsController { get; set; }

        [Inject]
        public IPersonSkillsController PersonSkillsController { get; set; }

        [Parameter]
        public int PersonId { get; set; }

        private List<SkillDto> Skills { get; set; }

        private List<PersonaSkillDto> PersonSkills { get; set; }


        async Task ToggleSelection(bool val, int skillId)
        {
            SkillCheckStates[skillId] = val;
            await PersonSkillsController.ToggleSkill(PersonId, skillId);
        }

        private async Task LoadData()
        {

            Skills = await SkillsController.GetSkills();
            PersonSkills = await PersonSkillsController.GetSkills(PersonId);

            List<string> categories = Skills.SelectMany(x => x.Categories).Distinct()
                .ToList();

            foreach (PersonaSkillDto personSkill in PersonSkills)
            {
                SkillCheckStates.Add(personSkill.SkillId, true);
            }

            foreach (string category in categories)
            {
                CategorySkills[category] = new Dictionary<string, int>();
            }

            foreach (SkillDto skill in Skills)
            {

                SkillCheckStates.TryAdd(skill.Id, false);

                foreach (string category in skill.Categories)
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

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            await LoadData();
        }
    }
}
