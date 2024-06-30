#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared;

public interface IDegree
{
    int SchoolId { get; set; }
    int Id { get; set; }
    string Name { get; set; }
}