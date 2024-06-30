#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared;

public interface IProject
{
    int Id { get; set; }
    int JobId { get; set; }
    int Order { get; set; }
    string Description { get; set; }
}