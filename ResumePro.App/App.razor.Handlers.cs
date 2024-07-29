#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Blazorise.Snackbar;
using EventAggregator.Blazor;
using ResumePro.Shared.Events;

namespace ResumePro.App;

public partial class App : IHandle<ResumeCreatedEvent>,
    IHandle<ResumeUpdatedEvent>,
    IHandle<ResumeDeletedEvent>,
    IHandle<ReferenceCreatedEvent>,
    IHandle<ReferenceUpdatedEvent>,
    IHandle<ReferenceDeletedEvent>,
    IHandle<SchoolCreatedEvent>,
    IHandle<SchoolUpdatedEvent>,
    IHandle<SchoolDeletedEvent>,
    IHandle<JobCreatedEvent>,
    IHandle<JobUpdatedEvent>,
    IHandle<JobDeletedEvent>,
    IHandle<CertificationCreatedEvent>,
    IHandle<CertificationUpdatedEvent>,
    IHandle<CertificationDeletedEvent>,
    IHandle<PersonCreatedEvent>,
    IHandle<PersonUpdatedEvent>,
    IHandle<PersonDeletedEvent>,
    IHandle<ResumeSettingsUpdatedEvent>
{
    public async Task HandleAsync(ResumeCreatedEvent message)
    {
        await HandleEvent(message);
    }

    public async Task HandleAsync(ResumeUpdatedEvent message)
    {
        await HandleEvent(message);
    }

    public async Task HandleAsync(ResumeDeletedEvent message)
    {
        await HandleEvent(message);
    }

    public async Task HandleAsync(ReferenceCreatedEvent message)
    {
        await HandleEvent(message);
    }

    public async Task HandleAsync(ReferenceUpdatedEvent message)
    {
        await HandleEvent(message);
    }

    public async Task HandleAsync(ReferenceDeletedEvent message)
    {
        await HandleEvent(message);
    }

    private async Task HandleEvent(BaseEvent raisedEvent)
    {
        await snackbarStack.PushAsync(raisedEvent.GetMessage(), SnackbarColor.Info);
    }

    public async Task HandleAsync(SchoolCreatedEvent message)
    {
        await HandleEvent(message);
    }

    public async Task HandleAsync(SchoolUpdatedEvent message)
    {
        await HandleEvent(message);
    }

    public async Task HandleAsync(SchoolDeletedEvent message)
    {
        await HandleEvent(message);
    }

    public async Task HandleAsync(JobCreatedEvent message)
    {
        await HandleEvent(message);
    }

    public async Task HandleAsync(JobUpdatedEvent message)
    {
        await HandleEvent(message);
    }

    public async Task HandleAsync(JobDeletedEvent message)
    {
        await HandleEvent(message);
    }

    public async Task HandleAsync(CertificationCreatedEvent message)
    {
        await HandleEvent(message);
    }

    public async Task HandleAsync(CertificationUpdatedEvent message)
    {
        await HandleEvent(message);
    }

    public async Task HandleAsync(CertificationDeletedEvent message)
    {
        await HandleEvent(message);
    }

    public async Task HandleAsync(PersonCreatedEvent message)
    {
        await HandleEvent(message);
    }

    public async Task HandleAsync(PersonUpdatedEvent message)
    {
        await HandleEvent(message);
    }

    public async Task HandleAsync(PersonDeletedEvent message)
    {
        await HandleEvent(message);
    }

    public async Task HandleAsync(ResumeSettingsUpdatedEvent message)
    {
        await HandleEvent(message);
    }
}