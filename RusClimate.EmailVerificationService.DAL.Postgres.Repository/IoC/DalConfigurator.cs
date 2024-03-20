using Microsoft.Extensions.DependencyInjection;
using RusClimate.EmailVerificationService.DAL.Postgres.Interface;

namespace RusClimate.EmailVerificationService.DAL.Postgres.Repository.IoC
{
    public static class DalConfigurator
    {
        public static IServiceCollection AddPostgresDal(this IServiceCollection services)
        {
            return services.AddScoped<IEmailRepository, EmailRepository>();
        }
    }
}
