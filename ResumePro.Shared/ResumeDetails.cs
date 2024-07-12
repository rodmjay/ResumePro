#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Extensions;

namespace ResumePro.Shared;

public class ResumeDetails : ResumeDto
{
    private List<JobDetails> _jobs;
    public int ResumeYears { get; set; }

    public List<JobDetails> Jobs
    {
        get
        {
            return _jobs.Where(x=>x.EndDate != null && (x.StartDate.Year > DateTime.Now.Year - Settings.ResumeYearHistory ||
                                                        x.EndDate.Value.Year > DateTime.Now.Year - Settings.ResumeYearHistory))
                .ToList();
        }
        set => _jobs = value;
    }

    public List<ResumeSkillDto> Skills { get; set; }
    public List<ReferenceDto> References { get; set; }
    public List<SchoolDetails> Education { get; set; }
    public List<PersonaLanguageDto> Languages { get; set; }
    public List<CertificationDto> Certifications { get; set; }

    public List<CategorySkillRating> SkillDictionary
    {
        get
        {
            List<CategorySkillRating> list = new();

            var categories = Skills.SelectMany(a => a.Categories).Distinct();

            foreach (var category in categories)
            {
                var skills = Skills.Where(x => x.Categories.Contains(category));

                list.Add(new CategorySkillRating
                {
                    Category = category,
                    Skills = skills.OrderByDescending(a => a.Rating)
                        .Select(x => new {x.Title, x.Rating}).ToList()
                });
            }

            return list;
        }
    }

    public string LanguageString => this.GetLanguageString();
}