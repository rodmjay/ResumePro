#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore;
using OpenAI.API;
using OpenAI.API.Completions;
using ResumePro.AI.Entities;
using ResumePro.Core.Services.Bases;
using ResumePro.Shared.Models;

namespace ResumePro.AI.Services;

public sealed class ChatGptService(IServiceProvider serviceProvider)
    : BaseService<ApiKey>(serviceProvider), IChatGptService
{
    private IQueryable<ApiKey> ApiKeys => Repository.Queryable();

    public async Task<ChatResult> Professionalize(int organizationId, int userId, ChatOptions options)
    {
        var retVal = new ChatResult();
        var apiKey = await ApiKeys
            .AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.UserId == userId)
            .Select(x => x.Key)
            .FirstOrDefaultAsync();

        if (apiKey == null)
            return retVal;

        var api = new OpenAIAPI(apiKey);

        var response = await api.Completions.CreateCompletionsAsync(new CompletionRequest
        {
            Model = "gpt-3.5-turbo-instruct",
            Prompt = "Rewrite the following text to sound more professional:\n\n" + options.InputText,
            MaxTokens = 180
        });

        return new ChatResult
        {
            OutputText = RemoveQuotes(response.Completions[0].Text.Trim())
        };
    }

    static string RemoveQuotes(string input)
    {
        return input.Trim('"', '\'');
    }
}