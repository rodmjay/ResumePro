#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Filters;

public class PersonaFilters
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int[] Skills { get; set; }
    public string State { get; set; }

    public string Country { get; set; }
}