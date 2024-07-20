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
[TestOf(typeof(HighlightsController))]
public class HighlightsControllerTest : BaseApiTest
{
    [TestFixture]
    public sealed class TheCreateHighlightMethod : HighlightsControllerTest
    {
        [TestCaseSource(typeof(HighlightsTestData), nameof(HighlightsTestData.ValidCreateOptions))]
        public async Task CanCreateHighlight(HighlightCreateOptions options)
        {
            var response = await HighlightsProxy.CreateHighlight(1, 1, options);
            Assert.That(response.Result is OkObjectResult, Is.True);

            var highlight = response.GetObject();
            Assert.That(highlight.Id, Is.GreaterThan(0));
            Assert.That(highlight.Text, Is.EqualTo(options.Text));
        }
    }

    [TestFixture]
    public sealed class TheUpdateHighlightMethod : HighlightsControllerTest
    {
        [TestCaseSource(typeof(HighlightsTestData), nameof(HighlightsTestData.ValidCreateOptions))]
        public async Task CanUpdateHighlight(HighlightCreateOptions options)
        {
            var response = await HighlightsProxy.CreateHighlight(1, 1, options);
            Assert.That(response.Result is OkObjectResult, Is.True);

            var highlightId = response.GetObject().Id;

            var updateOptions = new HighlightUpdateOptions
            {
                Order = 1,
                Text = options.Text + "_updated"
            };

            var updateResponse = await HighlightsProxy.UpdateHighlight(1, 1, highlightId, updateOptions);
            Assert.That(updateResponse.Result is OkObjectResult, Is.True);

            var highlight = updateResponse.GetObject();
            Assert.That(highlight.Text, Is.EqualTo(updateOptions.Text));
            Assert.That(highlight.Order, Is.EqualTo(updateOptions.Order));
        }
    }

    [TestFixture]
    public sealed class TheGetHighlightsMethod : HighlightsControllerTest
    {
        [Test]
        public async Task CanGetHighlights()
        {
            var response = await HighlightsProxy.GetHighlights(1, 2);
            Assert.That(response.Count, Is.GreaterThan(0));
        }
    }

    [TestFixture]
    public sealed class TheGetHighlightMethod : HighlightsControllerTest
    {
        [TestCaseSource(typeof(HighlightsTestData), nameof(HighlightsTestData.ValidCreateOptions))]
        public async Task CanGetHighlight(HighlightCreateOptions options)
        {
            var response = await HighlightsProxy.CreateHighlight(1, 1, options);
            Assert.That(response.Result is OkObjectResult, Is.True);

            var highlightId = response.GetObject().Id;

            var highlight = await HighlightsProxy.GetHighlight(1, 1, highlightId);
            Assert.That(highlight.Id, Is.EqualTo(highlightId));
            Assert.That(highlight.Text, Is.EqualTo(options.Text));
        }
    }

    [TestFixture]
    public sealed class TheDeleteHighlightMethod : HighlightsControllerTest
    {
        [TestCaseSource(typeof(HighlightsTestData), nameof(HighlightsTestData.ValidCreateOptions))]
        public async Task CanDeleteHighlight(HighlightCreateOptions options)
        {
            var response = await HighlightsProxy.CreateHighlight(1, 1, options);
            Assert.That(response.Result is OkObjectResult, Is.True);

            var highlightId = response.GetObject().Id;

            var result = await HighlightsProxy.DeleteHighlight(1, 1, highlightId);
            Assert.That(result.Succeeded, Is.EqualTo(true));
        }
    }
}