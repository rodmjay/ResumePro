﻿namespace ResumePro.Shared.Events;

public abstract class BaseEvent(EventType eventType)
{
    public EventType EventType { get; } = eventType;

    protected abstract string Name { get; }
    
    public string GetMessage()
    {
        return EventType switch
        {
            EventType.Updated => BuildUpdatedMessage(Name),
            EventType.Created => BuildCreatedMessage(Name),
            _ => BuildDeletedMessage(Name)
        };
    }

    protected string BuildCreatedMessage(string message)
    {
        return $"{message} successfully created";
    }

    private string BuildUpdatedMessage(string message)
    {
        return $"{message} successfully updated";
    }

    private string BuildDeletedMessage(string message)
    {
        return $"{message}  successfully deleted";
    }
}