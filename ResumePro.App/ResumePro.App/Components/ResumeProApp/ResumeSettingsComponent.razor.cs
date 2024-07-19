using Microsoft.AspNetCore.Components;
using ResumePro.Shared.Models;

namespace ResumePro.App.Components.ResumeProApp
{
    public partial class ResumeSettingsComponent
    {
        [Parameter]
        public ResumeSettingsDto Settings { get; set; }

        Validations? validationsRef;

        async Task Submit()
        {
            if (await validationsRef!.ValidateAll())
            {
                // Assuming you have a method to save the settings
                await SaveSettings();
                await validationsRef.ClearAll();
            }
        }

        private Task SaveSettings()
        {
            // Implement your save logic here
            throw new NotImplementedException();
        }
    }
}
