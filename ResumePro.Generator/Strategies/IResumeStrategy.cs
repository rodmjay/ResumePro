#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared;

namespace ResumePro.Generator.Strategies;

internal interface IResumeStrategy
{
    void ExecuteOperation(ResumeDetails resume);
}