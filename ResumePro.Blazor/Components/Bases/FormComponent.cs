#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Components;
using ResumePro.Shared.Common;

namespace ResumePro.Blazor.Components.Bases;

public abstract class FormComponent<TOptions> : ComponentBase where TOptions : class, new()
{
    public void ClearErrors()
    {
        Form.ClearErrors();
    }
    public void HandleErrors(Result result)
    {
        Form.HandleErrors(result);
    }

    public FormCard<TOptions> Form { get; set; }
    
    [Parameter] public string Title { get; set; }
    [Parameter] public TOptions Options { get; set; }
    [Parameter] public EventCallback<TOptions> OnSaved { get; set; }
    [Parameter] public EventCallback OnCancelled { get; set; }
    [Parameter] public EventCallback OnDeleted { get; set; }
}