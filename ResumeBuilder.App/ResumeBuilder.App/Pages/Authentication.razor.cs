#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Components;

namespace ResumeBuilder.App.Pages;

public partial class Authentication
{
    [Parameter] public string Action { get; set; }
}