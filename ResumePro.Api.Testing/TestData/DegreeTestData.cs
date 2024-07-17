using ResumePro.Shared.Options;

namespace ResumePro.Api.Testing.TestData;

public class DegreeTestData
{
    public static IEnumerable<DegreeOptions> ValidOptions()
    {
        return new List<DegreeOptions>()
        {
            new()
            {
                Name = "test"
            }
        };
    }
}