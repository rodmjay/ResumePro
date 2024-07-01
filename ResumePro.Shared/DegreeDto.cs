#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared;

public class DegreeDto : IDegree
{
    [JsonIgnore] public int SchoolId { get; set; }

    [JsonIgnore] public int Id { get; set; }

    public string Name { get; set; }
}