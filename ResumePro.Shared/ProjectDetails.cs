#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared;

public class ProjectDetails : ProjectDto
{
    public List<HighlightDto> Highlights { get; set; }
}