#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore.Design;
using ResumePro.Context;

namespace ResumePro.Factories;

public interface IApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
}