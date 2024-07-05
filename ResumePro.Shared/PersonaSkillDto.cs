using ResumePro.Shared.Interfaces;

namespace ResumePro.Shared;

public class PersonaSkillDto : IPersonaSkill
{
    [JsonIgnore]
    public virtual int PersonaId { get; set; }
    
    public virtual int SkillId { get; set; }
    public string Name { get; set; }

    public virtual int Rating { get; set; }
}