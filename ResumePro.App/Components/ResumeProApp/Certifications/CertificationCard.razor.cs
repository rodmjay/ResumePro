#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Components;
using ResumePro.Shared.Models;

namespace ResumePro.App.Components.ResumeProApp.Certifications;

public partial class CertificationCard
{
    [Parameter] public CertificationDto Certification { get; set; }
}