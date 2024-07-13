using ResumePro.Shared.Common;

namespace ResumePro.ErrorDescribers;

public class TemplateErrorDescriber
{
    public virtual Error TemplateNotFound(string templateId)
    {
        return new Error()
        {
            Code = nameof(TemplateNotFound)
        };
    }
}