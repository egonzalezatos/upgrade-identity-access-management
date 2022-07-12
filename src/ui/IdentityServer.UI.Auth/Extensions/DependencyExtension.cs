using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.UI.Auth.Extensions
{
    public static class DependencyExtension
    {
        public static IServiceCollection AddAuthUi(this IServiceCollection services)
        {
            services.AddControllersWithViews();
            return services;
        }

        public static IApplicationBuilder UseAuthUi(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
            return app;
        }
    }
}