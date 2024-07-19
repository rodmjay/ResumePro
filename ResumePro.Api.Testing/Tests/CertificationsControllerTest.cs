#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ResumePro.Api.Controllers;
using ResumePro.Api.Testing.Extensions;
using ResumePro.Api.Testing.TestData;
using ResumePro.Shared.Options;

namespace ResumePro.Api.Testing.Tests;

[TestFixture]
[TestOf(typeof(CertificationsController))]
public class CertificationsControllerTest : BaseApiTest
{
    [TestFixture]
    public sealed class TheCreateCertificationMethod : CertificationsControllerTest
    {
        [TestCaseSource(typeof(CertificationsTestData), nameof(CertificationsTestData.ValidOptions))]
        public async Task CanCreateCertification(CertificationOptions options)
        {
            var response = await CertificationsProxy.CreateCertification(1, options);
            Assert.That(response.Result is OkObjectResult, Is.True);

            var certification = response.GetObject();
            
            Assert.That(certification.Name, Is.EqualTo(options.Name));
            Assert.That(certification.Body, Is.EqualTo(options.Body));
            Assert.That(certification.Date, Is.EqualTo(options.Date));
            Assert.That(certification.Id, Is.GreaterThan(0));
        }
    }
}