namespace ResumePro.Services.ErrorDescribers;

public class ResumeErrorDescriber
{
    public virtual Error ResumeNotFound(int resumeId)
    {
        return new Error
        {
            Code = nameof(ResumeNotFound),
            Description = null
        };
    }

    public virtual Error UnableToSaveResume()
    {
        return new Error
        {
            Code = nameof(UnableToSaveResume),
            Description = null
        };
    }
}