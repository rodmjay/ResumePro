using Microsoft.AspNetCore.Components;
using ResumePro.Shared.Models;

namespace ResumePro.App.Components.ResumeProApp.Resumes
{
    public partial class ResumeSkillTableComponent
    {
        [Parameter]
        public List<ResumeSkillDto> ResumeSkills { get; set; }

        private Dictionary<string, List<string>> SkillDictionary { get; set; } = new();

        protected override void OnParametersSet()
        {
            var categories = ResumeSkills.SelectMany(x => x.Categories).Distinct().ToList();
            
            foreach (var category in categories)
            {
                SkillDictionary[category] = new List<string>();
            }

            foreach (var skill in ResumeSkills)
            {
                foreach (var category in skill.Categories)
                {
                    SkillDictionary[category].Add(skill.Title);
                }
            }
        }
    }
}
