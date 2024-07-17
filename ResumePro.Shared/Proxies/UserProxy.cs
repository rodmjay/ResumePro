#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Interfaces;

namespace ResumePro.Shared.Proxies;

public class UserProxy(HttpClient httpClient) : BaseProxy(httpClient), IUserController
{
    public async Task<UserOutput> GetUser()
    {
        throw new NotImplementedException();
    }
}