#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using NUnit.Framework;

namespace ResumePro.Api.Testing.Tests;

[TestFixture]
public class HighlightsControllerTest : BaseApiTest
{
    [TestFixture]
    public class TheGetHighlightsMethod : HighlightsControllerTest
    {
        [Test]
        public async Task CanGetHighlights()
        {
            var response = await HighlightsProxy.GetHighlights(1, 2);
            Assert.That(response.Count, Is.GreaterThan(0));
        }
    }
}