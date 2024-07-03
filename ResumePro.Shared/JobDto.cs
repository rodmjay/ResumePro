#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared;

[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class JobDto : IJob
{
    [JsonIgnore] public int Id { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Title { get; set; }
    public string Company { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
}