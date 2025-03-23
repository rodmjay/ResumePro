namespace ResumePro.Services.ErrorDescribers;

public class LanguageErrorDescriber
{
    public virtual Error LanguageNotFound(string languageId)
    {
        return new Error
        {
            Code = nameof(LanguageNotFound),
            Description = null
        };
    }
}