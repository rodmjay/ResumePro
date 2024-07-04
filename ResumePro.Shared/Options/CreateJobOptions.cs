#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Options;

public class CreateJobOptions
{
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Company { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
}