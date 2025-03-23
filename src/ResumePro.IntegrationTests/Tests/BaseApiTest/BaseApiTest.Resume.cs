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
        protected async Task<ResumeDetails> AssertGetResume(int personId, int resumeId)
        {
            var resume = await ResumeController.GetResume(personId, resumeId);
            Assert.That(resume, Is.Not.Null, "Failed to retrieve resume");
            return resume;
        }

        protected async Task<List<ResumeDto>> AssertGetResumes(int personId)
        {
            var resumes = await ResumeController.GetResumes(personId);
            Assert.That(resumes, Is.Not.Null, "Failed to retrieve resumes");
            return resumes;
        }

        protected async Task<ResumeDetails> AssertCreateResume(int personId, ResumeOptions options)
        {
            var response = await ResumeController.CreateResume(personId, options);
            Assert.That(response.IsSuccessStatusCode(), "Resume creation failed");
            var resume = response.GetObject<ResumeDetails>();
            return resume;
        }

        protected async Task<ResumeDetails> AssertUpdateResume(int personId, int resumeId, ResumeOptions options)
        {
            var response = await ResumeController.UpdateResume(personId, resumeId, options);
            Assert.That(response.IsSuccessStatusCode(), "Resume update failed");
            var resume = response.GetObject<ResumeDetails>();
            return resume;
        }

        protected async Task<Result> AssertDeleteResume(int personId, int resumeId)
        {
            var result = await ResumeController.DeleteResume(personId, resumeId);
            Assert.That(result.Succeeded, "Resume deletion failed");
            return result;
        }
    }
}
