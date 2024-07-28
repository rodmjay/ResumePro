#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Components;

namespace ResumePro.App.Components;

public partial class FormCard<TOptions> : ComponentBase where TOptions : class, new()
{
    protected Validations validationsRef;
    [Parameter] public string Title { get; set; }
    [Parameter] public RenderFragment ChildContent { get; set; }
    [Parameter] public TOptions Options { get; set; }
    [Parameter] public EventCallback<TOptions> OnSaved { get; set; }
    [Parameter] public EventCallback OnCancelled { get; set; }

    [Parameter] public EventCallback OnDeleted { get; set; }

    private void Cancel()
    {
        OnCancelled.InvokeAsync();
    }

    private void Delete()
    {
        OnDeleted.InvokeAsync();
    }

    private async Task HandleValidSubmit()
    {
        if (await validationsRef.ValidateAll()) await OnSaved.InvokeAsync(Options);
    }
}