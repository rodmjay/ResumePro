#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Proxies;

public interface IUserController
{
    Task<UserOutput> GetUser();
}