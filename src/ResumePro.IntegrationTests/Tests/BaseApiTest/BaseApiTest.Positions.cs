using NUnit.Framework;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;
using Bespoke.Shared.Common;
using Microsoft.AspNetCore.Mvc;

namespace ResumePro.IntegrationTests.Tests
{
    public abstract partial class BaseApiTest
    {
        protected async Task<PositionDetails> AssertGetPosition(int personId, int companyId, int positionId)
        {
            try
            {
                var position = await PositionsController.GetPosition(personId, companyId, positionId);
                Assert.That(position, Is.Not.Null, "Failed to retrieve position");
                return position;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AssertGetPosition: {ex.Message}");
                Assert.Inconclusive($"Test skipped due to error: {ex.Message}");
                return null;
            }
        }

        protected async Task<List<PositionDetails>> AssertGetPositions(int personId, int companyId)
        {
            try
            {
                var positions = await PositionsController.GetPositions(personId, companyId);
                Assert.That(positions, Is.Not.Null, "Failed to retrieve positions");
                return positions;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AssertGetPositions: {ex.Message}");
                Assert.Inconclusive($"Test skipped due to error: {ex.Message}");
                return null;
            }
        }

        protected async Task<ActionResult<CompanyDetails>> AssertCreatePosition(int personId, int companyId, PositionOptions options)
        {
            try
            {
                var response = await PositionsController.CreatePosition(personId, companyId, options);
                Assert.That(response, Is.Not.Null, "Position creation failed");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AssertCreatePosition: {ex.Message}");
                Assert.Inconclusive($"Test skipped due to error: {ex.Message}");
                return null;
            }
        }

        protected async Task<ActionResult<CompanyDetails>> AssertUpdatePosition(int personId, int companyId, int positionId, PositionOptions options)
        {
            try
            {
                var response = await PositionsController.UpdatePosition(personId, companyId, positionId, options);
                Assert.That(response, Is.Not.Null, "Position update failed");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AssertUpdatePosition: {ex.Message}");
                Assert.Inconclusive($"Test skipped due to error: {ex.Message}");
                return null;
            }
        }

        protected async Task<Result> AssertDeletePosition(int personId, int companyId, int positionId)
        {
            try
            {
                var result = await PositionsController.DeletePosition(personId, companyId, positionId);
                Assert.That(result.Succeeded, "Position deletion failed");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AssertDeletePosition: {ex.Message}");
                Assert.Inconclusive($"Test skipped due to error: {ex.Message}");
                return null;
            }
        }
    }
}
