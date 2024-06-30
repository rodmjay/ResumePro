#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Entities;

namespace ResumePro.Shared;

public class JobDto : IJob
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Title { get; set; }
    public string Company { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
}