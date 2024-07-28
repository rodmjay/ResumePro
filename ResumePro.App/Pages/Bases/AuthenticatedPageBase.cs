#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using EventAggregator.Blazor;
using Microsoft.AspNetCore.Components;

namespace ResumePro.App.Pages.Bases;

public abstract class AuthenticatedPageBase : ComponentBase
{
    [Inject] public IMapper Mapper { get; set; }

    [CascadingParameter]
    protected IEventAggregator EventAggregator { get; set; }

    [Inject]
    protected NavigationManager NavigationManager { get; set; }

}