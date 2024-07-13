using ResumePro.Shared.Common;

namespace ResumePro.ErrorDescribers;

public class PersonErrorDescriber
{
    public virtual Error PersonNotFound(int personId)
    {
        return new Error()
        {
            Code = nameof(PersonNotFound)
        };
    }

    public virtual Error UnableToSavePerson()
    {
        return new Error()
        {
            Code = nameof(UnableToSavePerson)
        };
    }
}