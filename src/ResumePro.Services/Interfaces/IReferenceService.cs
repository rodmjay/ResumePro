using Bespoke.Services.Interfaces;
using ResumePro.Entities;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Services.Interfaces;

public interface IReferenceService : IService<Reference>
{
    Task<List<T>> GetReferences<T>(int organizationId, int personId) where T : ReferenceDto;
    Task<T> GetReference<T>(int organizationId, int personId, int referenceId) where T : ReferenceDto;
    Task<OneOf<ReferenceDto, Result>> CreateReference(int organizationId, int personId, ReferenceOptions options);

    Task<OneOf<ReferenceDto, Result>> UpdateReference(int organizationId, int personId, int referenceId,
        ReferenceOptions options);

    Task<Result> DeleteReference(int organizationId, int personId, int referenceId);
}