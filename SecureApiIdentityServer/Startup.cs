using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IdentityServer4.Validation;
using IdentityServer4.Services;
using DataAccess;
using DataAccess.DataAccess;
using Business;
using DataAccess.IdentiyModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using SecureApiIdentityServer.AuthorizationHandler;

namespace SecureApiIdentityServer
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<TAD>(_ => new TAD(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("permission1", policy => policy.RequireClaim("permission", "ViewUsers"));
            //    options.AddPolicy("permission2", policy => policy.RequireClaim("permission", "AddUsers"));
            //    options.AddPolicy("permission3", policy => policy.RequireClaim("permission", "DeleteUsers"));
            //});

            services.AddIdentityServer()
             .AddTemporarySigningCredential()
             .AddInMemoryClients(ISConfig.GetClients())
             .AddInMemoryIdentityResources(ISConfig.GetIdentityResources())
             .AddInMemoryApiResources(ISConfig.GetApiResources());
             

            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>()
             .AddTransient<IProfileService, ProfileService>();

            services.AddSingleton<IAuthorizationHandler, HasPermissionHandler>();

            services.AddSingleton<IUserAccess, UserAccess>();
            services.AddSingleton<IUserManager, UserManager>();

            // Enable cors if required
            services.AddCors();

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIdentityServer();

            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                Authority = "http://localhost:59330",
                RequireHttpsMetadata = false,
                
                ApiName = "api1"
            });

            // Configure Cors if enabled
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseMvc();

        }
    }
}
