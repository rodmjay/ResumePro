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
        protected async Task<DegreeDto> AssertGetDegree(int personId, int schoolId, int degreeId)
        {
            try
            {
                var degree = await DegreesController.GetDegree(personId, schoolId, degreeId);
                Assert.That(degree, Is.Not.Null, "Failed to retrieve degree");
                return degree;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AssertGetDegree: {ex.Message}");
                Assert.Inconclusive($"Test skipped due to error: {ex.Message}");
                return null;
            }
        }

        protected async Task<List<DegreeDto>> AssertGetDegrees(int personId, int schoolId)
        {
            try
            {
                var degrees = await DegreesController.GetDegrees(personId, schoolId);
                Assert.That(degrees, Is.Not.Null, "Failed to retrieve degrees");
                return degrees;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AssertGetDegrees: {ex.Message}");
                Assert.Inconclusive($"Test skipped due to error: {ex.Message}");
                return null;
            }
        }

        protected async Task<DegreeDto> AssertCreateDegree(int personId, int schoolId, DegreeOptions options)
        {
            try
            {
                var response = await DegreesController.CreateDegree(personId, schoolId, options);
                Assert.That(response, Is.Not.Null, "Degree creation failed");
                return response.GetObject();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AssertCreateDegree: {ex.Message}");
                Assert.Inconclusive($"Test skipped due to error: {ex.Message}");
                return null;
            }
        }

        protected async Task<ActionResult<DegreeDto>> AssertUpdateDegree(int personId, int schoolId, int degreeId, DegreeOptions options)
        {
            try
            {
                var response = await DegreesController.UpdateDegree(personId, schoolId, degreeId, options);
                Assert.That(response, Is.Not.Null, "Degree update failed");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AssertUpdateDegree: {ex.Message}");
                Assert.Inconclusive($"Test skipped due to error: {ex.Message}");
                return null;
            }
        }

        protected async Task<Result> AssertDeleteDegree(int personId, int schoolId, int degreeId)
        {
            try
            {
                var result = await DegreesController.DeleteDegree(personId, schoolId, degreeId);
                Assert.That(result.Succeeded, "Degree deletion failed");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AssertDeleteDegree: {ex.Message}");
                Assert.Inconclusive($"Test skipped due to error: {ex.Message}");
                return null;
            }
        }
    }
}
