#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Entities;

public interface IJob
{
    int Id { get; set; }
    DateTime StartDate { get; set; }
    DateTime? EndDate { get; set; }
    string Title { get; set; }
    string Company { get; set; }
    string Location { get; set; }
    string Description { get; set; }
}