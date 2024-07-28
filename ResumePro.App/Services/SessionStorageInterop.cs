#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.JSInterop;

namespace ResumePro.App.Services;

public class SessionStorageInterop(IJSRuntime jsRuntime)
{
    public async Task<T> LoadFromSessionStorage<T>(string key)
    {
        T value = await jsRuntime.InvokeAsync<T>("blazorSessionStorage.getItem", key);
        return value;
    }

    public async Task SaveToSessionStorage<T>(string key, T value)
    {
        await jsRuntime.InvokeVoidAsync("blazorSessionStorage.setItem", key, value.ToString());
    }
}