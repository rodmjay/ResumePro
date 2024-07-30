using Microsoft.AspNetCore.Components;
using ResumePro.Shared.Models;

namespace ResumePro.Blazor.Components.Resumes
{
    public partial class ResumeSkillTableComponent
    {
        [Parameter]
        public List<ResumeSkillDto> ResumeSkills { get; set; }

        private Dictionary<string, List<string>> SkillDictionary { get; set; } = new();

        protected override void OnParametersSet()
        {
            List<string> categories = ResumeSkills.SelectMany(x => x.Categories).Distinct().ToList();
            
            foreach (string category in categories)
            {
                SkillDictionary[category] = new List<string>();
            }

            foreach (ResumeSkillDto skill in ResumeSkills)
            {
                foreach (string category in skill.Categories)
                {
                    SkillDictionary[category].Add(skill.Title);
                }
            }
        }
    }
}
