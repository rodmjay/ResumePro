#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Generation;

public interface IResumeGenerator
{
    string ExecuteOperation(ResumeDetails resume);
}