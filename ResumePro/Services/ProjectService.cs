#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Services.Bases;
using ResumePro.Entities;
using ResumePro.Interfaces;

namespace ResumePro.Services;

public class ProjectService : BaseService<Project>, IProjectService
{
    public ProjectService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}