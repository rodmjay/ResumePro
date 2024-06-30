#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared;

public class ProjectDto : IProject
{
    public int Id { get; set; }
    public int JobId { get; set; }
    public int Order { get; set; }
    public string Description { get; set; }
}