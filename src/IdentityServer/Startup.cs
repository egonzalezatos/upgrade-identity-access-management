using System;
using IdentityServer.Infrastructure.Configuration.Extensions;
using IdentityServer.IoC;
using IdentityServer.Options.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            //Run Configurations
            Configuration.ReadConfigurationEnvironments();
            
            services.AddDependencies(Configuration);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IServiceProvider provider, IWebHostEnvironment env)
        {
            //Execute migrations
            app.RunMigrations(Configuration);
            
            app.UsePipelineConfiguration(provider, env);
        }
    }
}
