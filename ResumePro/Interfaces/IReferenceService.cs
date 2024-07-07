using OneOf;
using ResumePro.Core.Services.Interfaces;
using ResumePro.Entities;
using ResumePro.Shared;
using ResumePro.Shared.Common;
using ResumePro.Shared.Options;

namespace ResumePro.Interfaces;

public interface IReferenceService : IService<Reference>
{
    Task<List<T>> GetReferences<T>(int organizationId, int personId) where T : ReferenceDto;
    Task<OneOf<ReferenceDto, Result>> CreateReference(int organizationId, int personId, ReferenceOptions options);
}