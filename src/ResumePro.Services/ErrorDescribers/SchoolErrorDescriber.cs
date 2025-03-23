namespace ResumePro.Services.ErrorDescribers;

public class SchoolErrorDescriber
{
    public virtual Error SchoolNotFound(int SchoolId)
    {
        return new Error
        {
            Code = nameof(SchoolNotFound),
            Description = null
        };
    }


    public virtual Error UnableToSaveSchool()
    {
        return new Error
        {
            Code = nameof(UnableToSaveSchool),
            Description = null
        };
    }
}