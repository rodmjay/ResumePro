#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Models;

namespace ResumePro.Shared.Interfaces;

public interface IUserController
{
    Task<UserOutput> GetUser();
}