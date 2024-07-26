#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Shared.Proxies;

public sealed class ChatGptProxy(HttpClient httpClient) : BaseProxy(httpClient), ITextController
{
    public async Task<ChatResult> Professionalize(ChatOptions options)
    {
        try
        {
            return await DoPost<ChatOptions, ChatResult>("v1.0/text", options)
                .ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}