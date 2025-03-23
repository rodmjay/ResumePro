using NUnit.Framework;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;
using Bespoke.IntegrationTesting.Extensions;
using Bespoke.Shared.Common;
using Microsoft.AspNetCore.Mvc;

namespace ResumePro.IntegrationTests.Tests
{
    public abstract partial class BaseApiTest
    {
        protected async Task<SchoolDetails> AssertGetSchool(int personId, int schoolId)
        {
            var school = await SchoolsController.GetSchool(personId, schoolId);
            Assert.That(school, Is.Not.Null, "Failed to retrieve school");
            return school;
        }

        protected async Task<List<SchoolDetails>> AssertGetSchools(int personId)
        {
            var schools = await SchoolsController.GetSchools(personId);
            Assert.That(schools, Is.Not.Null, "Failed to retrieve schools");
            return schools;
        }

        protected async Task<SchoolDetails> AssertCreateSchool(int personId, SchoolOptions options)
        {
            var response = await SchoolsController.CreateSchool(personId, options);
            Assert.That(response.IsSuccessStatusCode(), "School creation failed");
            var school = response.GetObject<SchoolDetails>();
            return school;
        }

        protected async Task<SchoolDetails> AssertUpdateSchool(int personId, int schoolId, SchoolOptions options)
        {
            var response = await SchoolsController.UpdateSchool(personId, schoolId, options);
            Assert.That(response.IsSuccessStatusCode(), "School update failed");
            var school = response.GetObject<SchoolDetails>();
            return school;
        }

        protected async Task<Result> AssertDeleteSchool(int personId, int schoolId)
        {
            var result = await SchoolsController.DeleteSchool(personId, schoolId);
            Assert.That(result.Succeeded, "School deletion failed");
            return result;
        }
    }
}
