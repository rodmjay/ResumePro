#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Models;

public class ProjectDetails : ProjectDto
{
    public List<HighlightDto> Highlights { get; set; }
}