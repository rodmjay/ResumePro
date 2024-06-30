namespace ResumePro.Shared
{
    public class ResumeDto : IResume
    {
        [JsonIgnore]
        public int PersonaId { get; set; }
        [JsonIgnore]
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
    }
}
