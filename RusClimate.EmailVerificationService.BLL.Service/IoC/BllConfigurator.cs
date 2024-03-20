using Microsoft.Extensions.DependencyInjection;
using RusClimate.EmailVerificationService.BLL.Interface;

namespace RusClimate.EmailVerificationService.BLL.Service.IoC
{
    public static class BllConfigurator
    {
        public static IServiceCollection AddBllModules(this IServiceCollection services)
        {
            return services.AddScoped<IEmailService, EmailService>();
        }
    }
}
