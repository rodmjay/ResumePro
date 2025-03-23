#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Models;

[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class ProjectDto : IProject
{
    public int Id { get; set; }

    [JsonIgnore] public int CompanyId { get; set; }

    public int Order { get; set; }

    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal? Budget { get; set; }
}