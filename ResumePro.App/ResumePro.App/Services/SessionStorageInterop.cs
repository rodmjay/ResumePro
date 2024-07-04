#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

using Microsoft.JSInterop;

namespace TranslationPro.Blazor.Services;
public class FormResponse
{
    public string Value { get; set; }
}
public class SessionStorageInterop
{
    private readonly IJSRuntime _jsRuntime;

    public SessionStorageInterop(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<T> LoadFromSessionStorage<T>(string key)
    {
        var value = await _jsRuntime.InvokeAsync<T>("blazorSessionStorage.getItem", key);
        return value;
    }

    public async Task SaveToSessionStorage<T>(string key, T value)
    {
        await _jsRuntime.InvokeVoidAsync("blazorSessionStorage.setItem", key, value.ToString());
    }
}