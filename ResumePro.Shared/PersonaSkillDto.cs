using ResumePro.Shared.Interfaces;

namespace ResumePro.Shared;

public class PersonaSkillDto : IPersonaSkill
{
    [JsonIgnore]
    public virtual int PersonaId { get; set; }

    [JsonIgnore]
    public virtual int SkillId { get; set; }
    public string SkillName { get; set; }

    public virtual int Rating { get; set; }
}