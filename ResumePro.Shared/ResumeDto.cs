namespace ResumePro.Shared
{
    public class ResumeDto : IResume
    {
        public int PersonaId { get; set; }
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
    }
}
