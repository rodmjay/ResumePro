#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Identity;
using ResumePro.Core.Services.Interfaces;
using ResumePro.Users.Entities;

namespace ResumePro.Users.Interfaces;

public interface IRoleService : IService<Role>,
    IRoleStore<Role>,
    IQueryableRoleStore<Role>,
    IRoleClaimStore<Role>
{
}