﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Components;
using ResumePro.Shared.Models;

namespace ResumePro.App.Components.Resumes;

public partial class ResumeContactInfoComponent
{
    [Parameter] public ResumeDto Resume { get; set; }
}