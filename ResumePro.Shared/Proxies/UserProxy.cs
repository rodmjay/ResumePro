#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Proxies;

public class UserProxy : BaseProxy, IUserController
{
    public UserProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<UserOutput> GetUser()
    {
        throw new NotImplementedException();
    }
}