#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Models;

public class DegreeDto : IDegree
{
    [JsonIgnore] public int SchoolId { get; set; }

    public int Id { get; set; }

    public string Name { get; set; }
    public int Order { get; set; }
}