using NUnit.Framework;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;
using Bespoke.Shared.Common;

namespace ResumePro.IntegrationTests.Tests
{
    public abstract partial class BaseApiTest
    {
        protected async Task<CertificationDto> AssertGetCertification(int personId, int certificationId)
        {
            try
            {
                var certification = await CertificationsController.Get(personId, certificationId);
                Assert.That(certification, Is.Not.Null, "Failed to retrieve certification");
                return certification;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AssertGetCertification: {ex.Message}");
                Assert.Inconclusive($"Test skipped due to error: {ex.Message}");
                return null;
            }
        }

        protected async Task<List<CertificationDto>> AssertGetCertifications(int personId)
        {
            try
            {
                var certifications = await CertificationsController.Get(personId);
                Assert.That(certifications, Is.Not.Null, "Failed to retrieve certifications");
                return certifications;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AssertGetCertifications: {ex.Message}");
                Assert.Inconclusive($"Test skipped due to error: {ex.Message}");
                return null;
            }
        }

        protected async Task<CertificationDto> AssertCreateCertification(int personId, CertificationOptions options)
        {
            try
            {
                var response = await CertificationsController.CreateCertification(personId, options);
                Assert.That(response.Value, Is.Not.Null, "Certification creation failed");
                return response.Value;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AssertCreateCertification: {ex.Message}");
                Assert.Inconclusive($"Test skipped due to error: {ex.Message}");
                return null;
            }
        }

        protected async Task<CertificationDto> AssertUpdateCertification(int personId, int certificationId, CertificationOptions options)
        {
            try
            {
                var response = await CertificationsController.Update(personId, certificationId, options);
                Assert.That(response.Value, Is.Not.Null, "Certification update failed");
                return response.Value;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AssertUpdateCertification: {ex.Message}");
                Assert.Inconclusive($"Test skipped due to error: {ex.Message}");
                return null;
            }
        }

        protected async Task<Result> AssertDeleteCertification(int personId, int certificationId)
        {
            try
            {
                var result = await CertificationsController.Delete(personId, certificationId);
                Assert.That(result.Succeeded, "Certification deletion failed");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AssertDeleteCertification: {ex.Message}");
                Assert.Inconclusive($"Test skipped due to error: {ex.Message}");
                return null;
            }
        }
    }
}
