#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Components;
using ResumePro.Shared.Common;

namespace ResumePro.App.Components;

public partial class FormCard<TOptions> : ComponentBase where TOptions : class, new()
{
    public List<Error> Errors { get; set; } = new();

    public void ClearErrors()
    {
        this.Errors.Clear();
    }
    public void HandleErrors(Result result)
    {
        this.Errors = result.Errors.ToList();
        StateHasChanged();
    }
    
    private bool showModal = false;
    
    private Validations validationsRef;

    
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
        if (await validationsRef.ValidateAll()) 
            await OnSaved.InvokeAsync(Options);
    }
}