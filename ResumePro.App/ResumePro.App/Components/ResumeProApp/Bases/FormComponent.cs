using Microsoft.AspNetCore.Components;
using ResumePro.Shared.Options;

namespace ResumePro.App.Components.ResumeProApp.Bases
{
    public abstract class FormComponent<TOptions> : ComponentBase where TOptions: class, new()
    {
        [Parameter]
        public TOptions Options { get; set; }


        [Parameter]
        public EventCallback<TOptions> OnSaved { get; set; }


        [Parameter]
        public EventCallback OnCancelled { get; set; }

        protected Validations validationsRef;
        protected void Cancel()
        {
            OnCancelled.InvokeAsync();
        }
        protected async Task HandleValidSubmit()
        {
            try
            {
                if (await validationsRef.ValidateAll())
                {
                    await OnSaved.InvokeAsync(Options);
                }
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("Validation error: " + ex.ParamName + " - " + ex.Message);
            }
           
        }
    }
}
