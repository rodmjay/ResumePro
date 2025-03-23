using Bespoke.IntegrationTesting.Bases;
using Microsoft.AspNetCore.Mvc;
using ResumePro.Api.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.IntegrationTests.Proxies;

public sealed class TextProxy : BaseProxy, ITextController
{
    private const string RootUrl = "v1.0/text";

    public TextProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<ChatResult> Professionalize([FromBody] ChatOptions options)
    {
        return await DoPostAsync<ChatOptions, ChatResult>($"{RootUrl}/professionalize", options);
    }
}
