﻿#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared;

[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class ProjectDto : IProject
{
    [JsonIgnore] public int Id { get; set; }

    [JsonIgnore] public int JobId { get; set; }

    [JsonIgnore] public int Order { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
    public decimal? Budget { get; set; }
}