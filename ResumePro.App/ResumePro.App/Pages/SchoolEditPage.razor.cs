﻿using AutoMapper;
using Microsoft.AspNetCore.Components;
using ResumePro.App.Pages.Bases;
using ResumePro.Shared.Extensions;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Pages
{
    public partial class SchoolEditPage : PersonPageBase
    {
        [Inject]
        public IMapper Mapper { get; set; }
        
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ISchoolsController SchoolsController { get; set; }
        
        [Parameter]
        public int SchoolId { get; set; }

        public SchoolDetails School { get; set; }

        public SchoolOptions Options { get; set; } = new SchoolOptions();

        protected override async Task OnParametersSetAsync()
        {
            School = await SchoolsController.GetSchool(PersonId, SchoolId);
            Options = Mapper.Map<SchoolOptions>(School);
            await base.OnParametersSetAsync();
        }


        private async Task HandleValidSubmit(SchoolOptions options)
        {
            var response = await SchoolsController.UpdateSchool(PersonId, SchoolId, options);
            if (response.IsSuccessStatusCode())
            {
                NavigationManager.NavigateTo($"/people/{PersonId}?tab=education");
            }
        }

        private void HandleCancelled()
        {
            NavigationManager.NavigateTo($"/people/{PersonId}?tab=education");
        }
    }
}