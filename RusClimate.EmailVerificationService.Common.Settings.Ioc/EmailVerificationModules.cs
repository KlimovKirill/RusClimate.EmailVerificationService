using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace RusClimate.EmailVerificationService.Common.Settings.Ioc
{
    public static class EmailVerificationModules
    {
        private static IServiceCollection AddEmailVerificationSettings(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var rawSettings = new EmailVerificationSettings.RawSettings();
            configuration.GetSection("EmailVerificationSettings").Bind(rawSettings);

            serviceCollection.TryAddSingleton(provider =>
                new EmailVerificationSettings(rawSettings));

            return serviceCollection;
        }
    }
}
