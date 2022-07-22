using System;
using IdentityServer.Infrastructure.Configuration.Extensions;
using IdentityServer.UI.Auth.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace IdentityServer.IoC
{
    internal static class RequestPipelineConfiguration
    {
        internal static IApplicationBuilder UsePipelineConfiguration(this IApplicationBuilder app, IServiceProvider serviceProvider, IWebHostEnvironment env)
        {
            app.UseInfrastructure(serviceProvider);
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IdentityServer v1"));
                app.UseCookiePolicy(new CookiePolicyOptions()
                {
                    MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Lax
                });
            }

            // app.UseHttpsRedirection();
            app.UseRouting();
            app.UseIdentityServer();
            
            app.UseAuthorization();
            app.UseUi();
            return app;
        }  
    }
}