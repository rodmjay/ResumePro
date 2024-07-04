using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace ResumePro.App.MessageHandlers
{
    public class ApiAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        public ApiAuthorizationMessageHandler(
            IAccessTokenProvider provider, NavigationManager navigation, IConfiguration config) 
            : base(provider, navigation)
        {
            ConfigureHandler(
                  authorizedUrls: new[] { config["ApiBase"] });
        }
    }
}
