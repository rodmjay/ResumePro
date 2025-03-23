namespace ResumePro.Services.ErrorDescribers;

public class ReferenceErrorDescriber
{
    public virtual Error ReferenceNotFound(int referenceId)
    {
        return new Error
        {
            Code = nameof(ReferenceNotFound),
            Description = null
        };
    }

    public virtual Error UnableToSaveReference()
    {
        return new Error
        {
            Code = nameof(UnableToSaveReference),
            Description = null
        };
    }
}