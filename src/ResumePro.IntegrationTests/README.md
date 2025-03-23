# ResumePro.IntegrationTests

Integration tests for the ResumePro API.

## Running Tests

To run all tests:

```bash
dotnet test
```

To run a specific test:

```bash
dotnet test --filter "FullyQualifiedName=ResumePro.IntegrationTests.Tests.Controllers.PeopleTests+GetPeopleMethodTests.GetPeople_ShouldReturnPeople"
```

## Code Coverage

This project includes code coverage tools to track test coverage.

### Prerequisites

Install the ReportGenerator global tool:

```bash
dotnet tool install -g dotnet-reportgenerator-globaltool
```

### Running Tests with Coverage

Use the provided script to run tests with coverage and generate a report:

```bash
./run-coverage.sh
```

This will:
1. Run all tests with coverage collection
2. Generate an HTML coverage report in the `./CoverageReport` directory

### Viewing Coverage Reports

Open `./CoverageReport/index.html` in a browser to view the coverage report.

## Test Structure

Tests are organized by controller with nested test fixtures for each API method:

```csharp
[TestFixture]
public class SomeControllerTests : BaseApiTest
{
    [TestFixture]
    private class SomeMethodTests : SomeControllerTests
    {
        [Test]
        public async Task MethodName_ShouldDoSomething()
        {
            // Test implementation
        }
    }
}
```
