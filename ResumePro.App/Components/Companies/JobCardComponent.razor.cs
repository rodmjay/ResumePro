#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Components;
using ResumePro.Shared.Models;

namespace ResumePro.App.Components.Companies;

public partial class JobCardComponent
{
    [Parameter] public CompanyDetails Company { get; set; }
}