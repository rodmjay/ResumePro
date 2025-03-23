using ResumePro.Shared.Options;

namespace ResumePro.IntegrationTests.TestData
{
    public class PersonData
    {
        public static IEnumerable<PersonOptions> GetValidPeopleOptions
        {
            get
            {
                yield return new PersonOptions
                {
                    FirstName = "Test",
                    LastName = "Person",
                    Email = $"test.person.{Guid.NewGuid()}@example.com",
                    PhoneNumber = "555-123-4567",
                    City = "Seattle",
                    StateId = 1,
                    GitHub = "https://github.com/testuser",
                    LinkedIn = "https://linkedin.com/in/testuser"
                };
            }
        }
    }
}
