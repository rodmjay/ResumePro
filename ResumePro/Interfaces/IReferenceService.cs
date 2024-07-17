#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Services.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Interfaces;

public interface IReferenceService : IService<Reference>
{
    Task<List<T>> GetReferences<T>(int organizationId, int personId) where T : ReferenceDto;
    Task<T> GetReference<T>(int organizationId, int personId, int referenceId) where T : ReferenceDto;
    Task<OneOf<ReferenceDto, Result>> CreateReference(int organizationId, int personId, CreateReferenceOptions options);

    Task<OneOf<ReferenceDto, Result>> UpdateReference(int organizationId, int personId, int referenceId,
        ReferenceOptions options);

    Task<Result> DeleteReference(int organizationId, int personId, int referenceId);
}