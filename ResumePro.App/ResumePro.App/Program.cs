using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ResumePro.App.Services;
using ResumePro.Shared.Policies;

namespace ResumePro.App;

public class Program
{
    public static async Task Main(string[] args)
    {

        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        builder.Services.AddMsalAuthentication(options =>
        {
            builder.Configuration.Bind("Authentication", options.ProviderOptions);
            options.ProviderOptions.DefaultAccessTokenScopes.Add("api1");
            options.ProviderOptions.LoginMode = "redirect";
        });

        
        AddBlazorise(builder.Services);

        await builder.Build().RunAsync();


        void AddBlazorise(IServiceCollection services)
        {
            services
                .AddBlazorise();
            services
                .AddBootstrap5Providers()
                .AddFontAwesomeIcons();

        }

    }
}