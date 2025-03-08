#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Services.Interfaces;

public interface IIdGenerationService
{
    Task<int> GetNextResumeId(int organizationId);
}