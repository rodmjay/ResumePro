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
[TestOf(typeof(ReferencesController))]
public class ReferencesControllerTest : BaseApiTest
{
    [TestFixture]
    public sealed class TheCreateReferenceMethod : ReferencesControllerTest
    {
        [TestCaseSource(typeof(ReferenceTestData), nameof(ReferenceTestData.ValidCreateOptions))]
        public async Task CanCreateReference(ReferenceOptions options)
        {
            var response = await ReferencesProxy.CreateReference(1, options);
            Assert.That(response.Result is OkObjectResult, Is.True);
        }
    }
}