#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Events;

public class ResumeSettingsUpdatedEvent : BaseEvent
{
    public ResumeSettingsUpdatedEvent(ResumeSettingsDto settings) : base(EventType.Updated)
    {
    }

    protected override string Name { get; } = "Resume Settings";
}