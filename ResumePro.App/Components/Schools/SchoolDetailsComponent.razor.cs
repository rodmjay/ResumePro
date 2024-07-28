#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Components;
using ResumePro.Shared.Models;

namespace ResumePro.App.Components.Schools;

public partial class SchoolDetailsComponent
{
    [Parameter] public SchoolDetails School { get; set; }
}