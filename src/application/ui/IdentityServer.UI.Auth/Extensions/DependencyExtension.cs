using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.UI.Auth.Extensions
{
    public static class DependencyExtension
    {
        public static IServiceCollection AddUi(this IServiceCollection services)
        {
            services.AddControllersWithViews();
            return services;
        }

        public static IApplicationBuilder UseUi(this IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
            return app;
        }
    }
}