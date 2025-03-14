﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Net;
using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Extensions;

namespace ResumePro.Shared.Proxies;

public abstract class BaseProxy
{
    protected readonly HttpClient HttpClient;

    protected BaseProxy(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }

    protected async Task<TOutput> DoPost<TOutput>(string url)
    {
        var response = await HttpClient.PostAsync(url, null).ConfigureAwait(false);

        var result = response.Content.DeserializeObject<TOutput>();
        return result;
    }

    protected async Task<TOutput> DoPost<TInput, TOutput>(string url, TInput input)
    {
        var content = input.SerializeToUTF8Json();
        var response = await HttpClient.PostAsync(url, content)
            .ConfigureAwait(false);

        var result = response.Content.DeserializeObject<TOutput>();
        return result;
    }

    protected async Task<ActionResult<TOutput>> DoPostActionResult<TOutput>(string url)
    {
        var response = await HttpClient.PostAsync(url, null)
            .ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
        {
            var result = response.Content.DeserializeObject<TOutput>();
            return new OkObjectResult(result);
        }
        else
        {
            var result = response.Content.DeserializeObject<Result>();
            return new BadRequestObjectResult(result);
        }
    }

    protected async Task<ActionResult<TOutput>> DoPostActionResult<TInput, TOutput>(string url, TInput input)
    {
        var inputContent = input!.SerializeToUTF8Json();
        var response = await HttpClient.PostAsync(url, inputContent)
            .ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
        {
            var result = response.Content.DeserializeObject<TOutput>();
            return new OkObjectResult(result);
        }
        else
        {
            var result = response.Content.DeserializeObject<Result>();
            return new BadRequestObjectResult(result);
        }
    }


    protected async Task<ActionResult<TOutput>> DoPutActionResult<TInput, TOutput>(string url, TInput input)
    {
        var inputContent = input!.SerializeToUTF8Json();
        var response = await HttpClient.PutAsync(url, inputContent)
            .ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
        {
            var result = response.Content.DeserializeObject<TOutput>();
            return new OkObjectResult(result);
        }
        else
        {
            var result = response.Content.DeserializeObject<Result>();
            return new BadRequestObjectResult(result);
        }
    }

    protected async Task<TOutput> DoGet<TOutput>(string url)
    {
        var response = await HttpClient.GetAsync(url)
            .ConfigureAwait(false);

        var result = response.Content.DeserializeObject<TOutput>();

        return result;
    }

    protected async Task<TOutput> DoPatch<TOutput>(string url)
    {
        var response = await HttpClient.PatchAsync(url, null)
            .ConfigureAwait(false);

        var result = response.Content.DeserializeObject<TOutput>();
        return result;
    }

    protected async Task<TOutput> DoPatch<TInput, TOutput>(string url, TInput input)
    {
        var content = input.SerializeToUTF8Json();
        var response = await HttpClient.PatchAsync(url, content)
            .ConfigureAwait(false);

        var result = response.Content.DeserializeObject<TOutput>();
        return result;
    }

    protected async Task<TOutput> DoPut<TOutput>(string url)
    {
        var response = await HttpClient.PutAsync(url, null)
            .ConfigureAwait(false);

        var result = response.Content.DeserializeObject<TOutput>();
        return result;
    }

    protected async Task<TOutput> DoPut<TInput, TOutput>(string url, TInput input)
    {
        var content = input.SerializeToUTF8Json();
        var response = await HttpClient.PutAsync(url, content)
            .ConfigureAwait(false);

        var result = response.Content.DeserializeObject<TOutput>();
        return result;
    }

    protected async Task<TOutput> DoDelete<TOutput>(string url)
    {
        var response = await HttpClient.DeleteAsync(url)
            .ConfigureAwait(false);

        var result = response.Content.DeserializeObject<TOutput>();
        return result;
    }

    protected async Task<ActionResult> DoActionResultGet(string url)
    {
        try
        {
            var response = await HttpClient.GetAsync(url).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                // Wrapping the stream in an OkObjectResult, which isn't typical for Blazor but follows your constraint
                return new OkObjectResult(stream);
            }
            else
            {
                return new NotFoundResult();
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions by returning an appropriate result
            return new BadRequestObjectResult(ex.Message);
        }
    }
}