#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Components;
using ResumePro.Shared.Models;

namespace ResumePro.Blazor.Components.Jobs;

public partial class JobDetailsComponent
{
    [Parameter] public JobDetails JobDetails { get; set; }
}