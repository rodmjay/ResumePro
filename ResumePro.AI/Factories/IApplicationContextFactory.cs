#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore.Design;
using ResumePro.AI.Contexts;

namespace ResumePro.AI.Factories;

public interface IApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
}