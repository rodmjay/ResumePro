#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared;

public class CompanyDetails : CompanyDto
{
    public List<HiringManagerDto> HiringManagers { get; set; }
}