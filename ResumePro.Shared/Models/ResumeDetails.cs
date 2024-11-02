#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Extensions;

namespace ResumePro.Shared.Models;

public class ResumeDetails : ResumeDto
{
    public List<CompanyDetails> Companies { get; set; }

    public List<ResumeSkillDto> Skills { get; set; }
    public List<ReferenceDto> References { get; set; }
    public List<SchoolDetails> Education { get; set; }
    public List<PersonaLanguageDto> Languages { get; set; }
    public List<CertificationDto> Certifications { get; set; }
    public List<RenderingDto> Renderings { get; set; }

    public List<CategorySkillRating> SkillDictionary
    {
        get
        {
            var categories = Skills.SelectMany(a => a.Categories).Distinct();

            return (from category in categories
                let skills = Skills.Where(x => x.Categories.Contains(category))
                select new CategorySkillRating
                {
                    Category = category,
                    Skills = skills.OrderByDescending(a => a.Rating)
                        .Select(x => new {x.Title, x.Rating})
                        .ToList()
                }).ToList();
        }
    }

    public string LanguageString => this.GetLanguageString();
}