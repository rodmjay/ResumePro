#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Models;

public class SchoolDetails : SchoolDto
{
    public List<DegreeDto> Degrees { get; set; }
}