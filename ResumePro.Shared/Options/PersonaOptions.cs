#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Diagnostics.CodeAnalysis;

namespace ResumePro.Shared.Options;

public class PersonaOptions
{
    [NotNull] public string City { get; set; }

    public int StateId { get; set; }

    [NotNull] public string FirstName { get; set; }

    [NotNull] public string LastName { get; set; }

    [NotNull] public string Email { get; set; }

    public string GitHub { get; set; }
    public string LinkedIn { get; set; }
}