#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Services.Bases;
using ResumePro.Entities;
using ResumePro.Interfaces;

namespace ResumePro.Services;

public class DegreeService : BaseService<Degree>, IDegreeService
{
    public DegreeService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}