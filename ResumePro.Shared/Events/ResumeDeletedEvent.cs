﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Events;

public class ResumeDeletedEvent() : BaseEvent(EventType.Deleted)
{
    protected override string Name => "Resume";
}