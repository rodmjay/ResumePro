#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Interfaces;

public interface IDegree
{
    int SchoolId { get; set; }
    int Id { get; set; }
    string Name { get; set; }
    int Order { get; set; }
}