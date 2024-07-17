using ResumePro.Shared.Options;

namespace ResumePro.Api.Testing.TestData;

public class ProjectTestData
{
    public static IEnumerable<ProjectOptions> ValidOptions()
    {
        return new List<ProjectOptions>()
        {
            new ProjectOptions()
            {
                Name = "test",
                Budget = null,
                Description = "foo",
                Order = 1
            }
        };
    }
}