﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Models;

namespace ResumePro.Shared.Interfaces;

public interface ICertification : IOrganizationId, IName, IPersonId, IId, IBody, IDate
{
}