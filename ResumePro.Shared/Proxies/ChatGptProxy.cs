#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Proxies;

public sealed class ChatGptProxy : BaseProxy, ITextController
{
    public ChatGptProxy(HttpClient httpClient) : base(httpClient)
    {
    }

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