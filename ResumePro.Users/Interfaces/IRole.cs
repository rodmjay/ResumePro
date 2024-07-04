#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Users.Interfaces;

public interface IRole
{
    string Name { get; set; }
    int Id { get; set; }
}