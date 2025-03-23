using ResumePro.Shared.Options;

namespace ResumePro.IntegrationTests.TestData;

public class SchoolData
{
    public static IEnumerable<SchoolOptions> GetValidSchoolOptions
    {
        get
        {
            yield return new SchoolOptions
            {
                Name = "Test University",
                Location = "Seattle",
                StartDate = DateTime.Now.AddYears(-4),
                EndDate = DateTime.Now.AddYears(-2)
            };
        }
    }
}