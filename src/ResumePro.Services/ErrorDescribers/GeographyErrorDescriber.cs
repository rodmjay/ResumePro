namespace ResumePro.Services.ErrorDescribers;

public class GeographyErrorDescriber
{
    public virtual Error StateNotFound()
    {
        return new Error
        {
            Code = nameof(StateNotFound),
            Description = "Unable to find state"
        };
    }

    public virtual Error EnableCountryError()
    {
        return new Error
        {
            Code = nameof(EnableCountryError),
            Description = "Unable to enable country"
        };
    }

    public virtual Error DisableCountryError()
    {
        return new Error
        {
            Code = nameof(DisableCountryError),
            Description = "Unable to disable country"
        };
    }

    public virtual Error CountryAlreadyEnabled()
    {
        return new Error
        {
            Code = nameof(CountryAlreadyEnabled),
            Description = "country already enabled"
        };
    }

    public virtual Error CountryAlreadyDisabled()
    {
        return new Error
        {
            Code = nameof(CountryAlreadyDisabled),
            Description = "country already disabled"
        };
    }
}