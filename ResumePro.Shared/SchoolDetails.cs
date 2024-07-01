#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared;

public class SchoolDetails : SchoolDto
{
    public List<DegreeDto> Degrees { get; set; }
}