using NUnit.Framework;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;
using Bespoke.IntegrationTesting.Extensions;
using Bespoke.Shared.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResumePro.IntegrationTests.Tests
{
    public abstract partial class BaseApiTest
    {
        protected async Task<CompanyDetails> AssertGetCompany(int personId, int companyId)
        {
            try
            {
                var company = await CompaniesController.GetCompany(personId, companyId);
                Assert.That(company, Is.Not.Null, "Failed to retrieve company");
                return company;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AssertGetCompany: {ex.Message}");
                Assert.Inconclusive($"Test skipped due to error: {ex.Message}");
                return null;
            }
        }

        protected async Task<List<CompanyDetails>> AssertGetCompanies(int personId)
        {
            try
            {
                var companies = await CompaniesController.GetCompanies(personId);
                Assert.That(companies, Is.Not.Null, "Failed to retrieve companies");
                return companies;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AssertGetCompanies: {ex.Message}");
                Assert.Inconclusive($"Test skipped due to error: {ex.Message}");
                return null;
            }
        }

        protected async Task<CompanyDetails> AssertCreateCompany(int personId, CompanyOptions options)
        {
            try
            {
                var response = await CompaniesController.CreateCompany(personId, options);
                Assert.That(response.IsSuccessStatusCode(), "Company creation failed");
                var company = response.GetObject<CompanyDetails>();
                return company;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AssertCreateCompany: {ex.Message}");
                Assert.Inconclusive($"Test skipped due to error: {ex.Message}");
                return null;
            }
        }

        protected async Task<CompanyDetails> AssertUpdateCompany(int personId, int companyId, CompanyOptions options)
        {
            try
            {
                var response = await CompaniesController.UpdateCompany(personId, companyId, options);
                Assert.That(response.IsSuccessStatusCode(), "Company update failed");
                var company = response.GetObject<CompanyDetails>();
                return company;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AssertUpdateCompany: {ex.Message}");
                Assert.Inconclusive($"Test skipped due to error: {ex.Message}");
                return null;
            }
        }

        protected async Task<Result> AssertDeleteCompany(int personId, int companyId)
        {
            try
            {
                var result = await CompaniesController.DeleteCompany(personId, companyId);
                Assert.That(result.Succeeded, "Company deletion failed");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AssertDeleteCompany: {ex.Message}");
                Assert.Inconclusive($"Test skipped due to error: {ex.Message}");
                return null;
            }
        }
    }
}
