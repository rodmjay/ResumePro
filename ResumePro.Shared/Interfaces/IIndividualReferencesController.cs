#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Interfaces;

public interface IIndividualReferencesController
{
    Task<ReferenceDto> Get(int referenceId);
    Task<List<ReferenceDto>> GetReferences();

    Task<ActionResult<ReferenceDto>> CreateReference(
        ReferenceOptions options);

    Task<ActionResult<ReferenceDto>> UpdateReference(
        int referenceId,
        ReferenceOptions options);

    Task<Result> DeleteReference(
        int referenceId);
}