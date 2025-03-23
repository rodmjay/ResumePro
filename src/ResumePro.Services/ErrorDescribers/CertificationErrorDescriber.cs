namespace ResumePro.Services.ErrorDescribers;

public class CertificationErrorDescriber
{
    public virtual Error CertificationNotFound(int certificationId)
    {
        return new Error
        {
            Description = "Certification not found",
            Code = nameof(CertificationNotFound)
        };
    }


    public virtual Error UnableToSaveCertification()
    {
        return new Error
        {
            Description = "Unable to save certification",
            Code = nameof(UnableToSaveCertification)
        };
    }
}