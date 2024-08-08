using Blazorise.Snackbar;
using Microsoft.AspNetCore.Components;

namespace ResumeBuilder.App
{
    public partial class App 
    {
        [Inject]
        public IToastService ToastService { get; set; }
        protected override void OnInitialized()
        {
            EventAggregator.Subscribe(this);
        }

        private readonly Theme theme = new()
        {
            BarOptions = new ThemeBarOptions
            {
                HorizontalHeight = "72px"
            },
            ColorOptions = new ThemeColorOptions
            {
                Primary = "#4A90E2",   // A fresh blue
                Secondary = "#D25627", // A warm, earthy orange
                Success = "#34A853",   // A vibrant green
                Info = "#50B5F1",      // A lighter, soothing blue
                Warning = "#FFAD42",   // A bright yellow-orange
                Danger = "#E6213D",    // A deep, alarming red
                Light = "#ECEFF1",     // A lighter grey, close to white
                Dark = "#212121"       // A darker, more intense grey
            },
            BackgroundOptions = new ThemeBackgroundOptions
            {
                Primary = "#4A90E2",   // Corresponding to Primary in ColorOptions
                Secondary = "#D25627", // Corresponding to Secondary in ColorOptions
                Success = "#34A853",   // Corresponding to Success in ColorOptions
                Info = "#50B5F1",      // Corresponding to Info in ColorOptions
                Warning = "#FFAD42",   // Corresponding to Warning in ColorOptions
                Danger = "#E6213D",    // Corresponding to Danger in ColorOptions
                Light = "#ECEFF1",     // Corresponding to Light in ColorOptions
                Dark = "#212121"       // Corresponding to Dark in ColorOptions
            },
            InputOptions = new ThemeInputOptions
            {
                CheckColor = "#4A90E2" // Corresponding to Primary in ColorOptions
            }
        };
        private Snackbar snackbarPrimary;
        SnackbarStack snackbarStack;
        double intervalBeforeMsgClose = 2000;
    }
}
