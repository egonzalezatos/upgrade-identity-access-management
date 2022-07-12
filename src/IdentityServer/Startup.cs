using System;
using IdentityServer.API.Extensions;
using IdentityServer.Configuration;
using IdentityServer.ExternalProviders.Extensions;
using IdentityServer.Infrastructure.Configuration.Extensions;
using IdentityServer.Security.Extensions;
using IdentityServer.UI.Auth.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace IdentityServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure(Configuration.GetDbConnection());
            services.AddAuthentication()
                .AddGoogleAuth();
            services.AddApi();
            services.AddCors(cfg =>
            {
                cfg.AddPolicy("AllowAll", p =>
                {
                    p.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "IAM";
            });
            
            services.AddAuthUi();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IdentityServer", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IServiceProvider provider, IWebHostEnvironment env)
        {
            //Execute migrations
            app.RunMigrations();
            app.UseInfrastructure(provider);
            
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
            app.UseStaticFiles();
            app.UseCors("AllowAll");
            //app.UseAuthentication();
            app.UseAuthorization();
            app.UseApi();
            app.UseAuthUi();
        }
    }
}
