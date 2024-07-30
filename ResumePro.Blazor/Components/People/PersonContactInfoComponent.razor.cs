#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Components;
using ResumePro.Shared.Models;

namespace ResumePro.Blazor.Components.People;

public partial class PersonContactInfoComponent
{
    [Parameter] public PersonaDto Person { get; set; }
}