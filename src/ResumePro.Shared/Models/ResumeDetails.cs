using ResumePro.Shared.Extensions;

namespace ResumePro.Shared.Models;

public class ResumeDetails : ResumeDto
{
    public List<CompanyDetails> Companies { get; set; } = new();

    public List<ResumeSkillDto> Skills { get; set; } = new();
    public List<ReferenceDto> References { get; set; } = new();
    public List<SchoolDetails> Education { get; set; } = new();
    public List<PersonaLanguageDto> Languages { get; set; } = new();
    public List<CertificationDto> Certifications { get; set; } = new();
    public List<RenderingDto> Renderings { get; set; } = new();

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
                        .Select(x => new { x.Title, x.Rating })
                        .ToList()
                }).ToList();
        }
    }

    public string? LanguageString => this.GetLanguageString();
}