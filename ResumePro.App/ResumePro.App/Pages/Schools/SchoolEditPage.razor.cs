#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using Microsoft.AspNetCore.Components;
using ResumePro.App.Pages.Bases;
using ResumePro.Shared.Extensions;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Pages.Schools;

public partial class SchoolEditPage : PersonPageBase
{
    [Inject] public IMapper Mapper { get; set; }

    [Inject] public NavigationManager NavigationManager { get; set; }

    [Inject] public ISchoolsController SchoolsController { get; set; }

    [Parameter] public int SchoolId { get; set; }

    public SchoolDetails School { get; set; }

    public SchoolOptions Options { get; set; } = new();

    protected override async Task OnParametersSetAsync()
    {
        School = await SchoolsController.GetSchool(PersonId, SchoolId);
        Options = Mapper.Map<SchoolOptions>(School);
        await base.OnParametersSetAsync();
    }

    private async Task HandleDelete()
    {
        var response = await SchoolsController.DeleteSchool(PersonId, SchoolId);
        if (response.Succeeded) NavigationManager.NavigateTo($"/people/{PersonId}?tab=education");
    }

    private async Task HandleValidSubmit(SchoolOptions options)
    {
        var response = await SchoolsController.UpdateSchool(PersonId, SchoolId, options);
        if (response.IsSuccessStatusCode()) NavigationManager.NavigateTo($"/people/{PersonId}?tab=education");
    }

    private void HandleCancelled()
    {
        NavigationManager.NavigateTo($"/people/{PersonId}?tab=education");
    }
}