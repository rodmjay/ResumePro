#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Components;

namespace ResumePro.App.Components.TodoApp;

public abstract class BaseTodoItems : ComponentBase
{
    protected string? description;

    protected Filter filter = Filter.All;

    protected List<Todo> todos = new()
    {
        new Todo {Description = "Buy milk"},
        new Todo {Description = "Call John regarding the meeting"},
        new Todo {Description = "Walk a dog"}
    };

    protected Validations? validations;

    protected IEnumerable<Todo> Todos
    {
        get
        {
            var query = from t in todos select t;

            if (filter == Filter.Active)
                query = from q in query where !q.Completed select q;

            if (filter == Filter.Completed)
                query = from q in query where q.Completed select q;

            return query;
        }
    }

    protected void SetFilter(Filter filter)
    {
        this.filter = filter;
    }

    protected void OnCheckAll(bool isChecked)
    {
        todos.ForEach(x => x.Completed = isChecked);
    }

    protected async Task OnAddTodo()
    {
        if (await validations!.ValidateAll())
        {
            todos.Add(new Todo {Description = description});
            description = null;

            await validations.ClearAll();
        }
    }

    protected void OnClearCompleted()
    {
        todos.RemoveAll(x => x.Completed);
        filter = Filter.All;
    }

    protected Task OnTodoStatusChanged(bool isChecked)
    {
        return InvokeAsync(StateHasChanged);
    }
}