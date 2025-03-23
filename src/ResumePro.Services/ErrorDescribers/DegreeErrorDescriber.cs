namespace ResumePro.Services.ErrorDescribers;

public class DegreeErrorDescriber
{
    public virtual Error DegreeNotFound(int degreeId)
    {
        return new Error
        {
            Code = nameof(DegreeNotFound),
            Description = null
        };
    }


    public virtual Error UnableToSaveDegree()
    {
        return new Error
        {
            Code = nameof(UnableToSaveDegree),
            Description = null
        };
    }
}