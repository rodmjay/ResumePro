﻿using Microsoft.AspNetCore.Components;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Pages
{
    public partial class TemplatesPage
    {
        [Inject]
        public ITemplatesController TemplatesController { get; set; }
        private TemplateDto SelectedTemplate;
        private TemplateOptions CreateTemplateOptions = new TemplateOptions();

        private void CreateNewTemplate()
        {
            SelectedTemplate = new TemplateDto
            {
                Name = "",
                Source = "",
                Format = "",
                Engine = "",
                IsGlobal = false,  // Default to editable
                Id = 0  // Signifying a new template
            };
            CreateTemplateOptions = new TemplateOptions
            {
                Name = "",
                Template = "",
                Format = "",
                Engine = ""
            };
        }

        private List<TemplateDto> TemplateRecords { get; set; }

        protected override async Task OnInitializedAsync()
        {
            TemplateRecords = await TemplatesController.GetTemplates();
        }
        private void SelectTemplate(TemplateDto template)
        {
            SelectedTemplate = template;
        }

        private async Task SaveTemplate(bool isNew)
        {
            //if (isNew)
            //{
            //    // POST request for new template using TemplateOptions
            //    var response = await Http.PostAsJsonAsync("api/templates", newTemplateOptions);
            //    // Handle response
            //}
            //else
            //{
            //    // PUT request for updating existing template using TemplateDto
            //    var response = await Http.PutAsJsonAsync($"api/templates/{selectedTemplate.Id}", selectedTemplate);
            //    // Handle response
            //}

            //// Refresh templates list or handle updates appropriately
            //templates = await Http.GetFromJsonAsync<List<TemplateDto>>("api/templates");
            SelectedTemplate = null;  // Reset or reselect template
        }
    }
}