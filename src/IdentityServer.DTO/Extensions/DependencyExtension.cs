using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.DTO.Extensions
{
    public static class DependencyExtension
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.Scan(selector => selector
                .FromAssembliesOf(typeof(DependencyExtension))
                .AddClasses(classes => classes.AssignableTo<IValidator>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            return services;
        }
    }
}