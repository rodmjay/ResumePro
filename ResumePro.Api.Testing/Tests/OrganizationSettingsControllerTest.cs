﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ResumePro.Api.Controllers;
using ResumePro.Api.Testing.TestData;
using ResumePro.Shared.Options;

namespace ResumePro.Api.Testing.Tests;

[TestFixture]
[TestOf(typeof(OrganizationSettingsController))]
public class OrganizationSettingsControllerTest : BaseApiTest
{
    [TestFixture]
    public sealed class TheUpdateOrganizationSettingsMethod : OrganizationSettingsControllerTest
    {
        [TestCaseSource(typeof(OrganizationSettingsTestData), nameof(OrganizationSettingsTestData.ValidOptions))]
        public async Task CanUpdateOrganizationSettings(OrganizationSettingsOptions options)
        {
            var response = await OrganizationSettingsProxy.UpdateSettings(options);
            Assert.That(response.Result is OkObjectResult, Is.True);
        }
    }

    [TestFixture]
    public sealed class TheCreateOrganizationSettingsMethod : OrganizationSettingsControllerTest
    {
        [TestCaseSource(typeof(OrganizationSettingsTestData), nameof(OrganizationSettingsTestData.ValidOptions))]
        public async Task CanCreateOrganizationSettings(OrganizationSettingsOptions options)
        {
            var response = await OrganizationSettingsProxy.CreateSettings(options);
            Assert.That(response.Result is OkObjectResult, Is.True);
        }
    }
}