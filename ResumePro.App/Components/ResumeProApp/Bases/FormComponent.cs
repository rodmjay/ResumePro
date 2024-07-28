#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Components;

namespace ResumePro.App.Components.ResumeProApp.Bases;

public abstract class FormComponent<TOptions> : ComponentBase where TOptions : class, new()
{
    [Parameter] public string Title { get; set; }

    [Parameter] public TOptions Options { get; set; }


    [Parameter] public EventCallback<TOptions> OnSaved { get; set; }


    [Parameter] public EventCallback OnCancelled { get; set; }

    [Parameter] public EventCallback OnDeleted { get; set; }
}