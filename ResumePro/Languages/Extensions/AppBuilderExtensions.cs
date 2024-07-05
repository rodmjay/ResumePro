#region Header

// /*

// Author: Rod Johnson, Architect, rodmjay@gmail.com
// */

#endregion

using Microsoft.Extensions.DependencyInjection.Extensions;
using ResumePro.Core.Middleware.Builders;
using ResumePro.Languages.Interfaces;
using ResumePro.Languages.Services;

namespace ResumePro.Languages.Extensions
{
    public static class AppBuilderExtensions
    {
        public static AppBuilder AddLanguageDependencies(this AppBuilder builder)
        {
            builder.Services.TryAddScoped<ILanguageService, LanguageService>();

            return builder;
        }
    }
}