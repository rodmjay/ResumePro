namespace ResumePro.Services.ErrorDescribers;

public class TemplateErrorDescriber
{
    public virtual Error TemplateNotFound(int templateId)
    {
        return new Error
        {
            Code = nameof(TemplateNotFound),
            Description = null
        };
    }
}