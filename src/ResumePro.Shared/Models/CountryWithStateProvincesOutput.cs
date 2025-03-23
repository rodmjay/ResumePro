namespace ResumePro.Shared.Models;

public class CountryWithStateProvincesOutput : CountryDetails
{
    public CountryWithStateProvincesOutput()
    {
        StateProvinces = new List<StateProvinceOutput>();
    }

    public List<StateProvinceOutput> StateProvinces { get; set; }
}