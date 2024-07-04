#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore.Design;
using ResumePro.Users.Contexts;

namespace ResumePro.Users.Factories;

public interface IApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
}