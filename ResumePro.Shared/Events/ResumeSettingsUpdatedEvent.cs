#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Models;

namespace ResumePro.Shared.Events;

public class ResumeSettingsUpdatedEvent(ResumeSettingsDto settings) : BaseEvent(EventType.Updated)
{
    protected override string Name { get; } = "Resume Settings";
}