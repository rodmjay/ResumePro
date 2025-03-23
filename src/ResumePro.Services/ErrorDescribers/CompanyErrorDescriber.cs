namespace ResumePro.Services.ErrorDescribers;

public class CompanyErrorDescriber
{
    public virtual Error CompanyNotFound(int companyId)
    {
        return new Error
        {
            Code = nameof(CompanyNotFound),
            Description = "Comany not found"
        };
    }


    public virtual Error UnableToSaveCompany()
    {
        return new Error
        {
            Code = nameof(UnableToSaveCompany),
            Description = "Unable to save company"
        };
    }
}