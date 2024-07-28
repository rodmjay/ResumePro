#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Models;

namespace ResumePro.Shared.Events;

public class ResumeUpdatedEvent(ResumeDetails resume) : BaseEvent(EventType.Updated)
{
    public ResumeDetails Resume { get; } = resume;
    protected override string Name { get; } = $"Resume: {resume.JobTitle}";
}