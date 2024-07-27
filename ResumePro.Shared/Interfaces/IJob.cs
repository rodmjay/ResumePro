#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Interfaces;

public interface IJob : IStartDate, IEndDate, IId
{
    string JobTitle { get; set; }
    string Company { get; set; }
    string Location { get; set; }
    string Description { get; set; }
}