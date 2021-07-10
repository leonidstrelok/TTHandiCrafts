using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TTHandiCrafts.Extensions;
using TTHandiCrafts.Infrastructure;
using TTHandiCrafts.Infrastructure.Interfaces.Interfaces;
using TTHandiCrafts.Models;
using TTHandiCrafts.Services;
using TTHandiCrafts.UseCases;
using DependencyInjection = TTHandiCrafts.UseCases.DependencyInjection;

namespace TTHandiCrafts
{
    public class Startup
    {
        private const string AllowedDomainsCorsPolicy = "AllowedDomains";

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        private IWebHostEnvironment Env { get; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);


            services
                .AddTTHandiCraftsUseCases()
                .AddTTHandiCraftsInfrastructure(Configuration)
                .AddTTHandiCraftsRequestLocalization()
                .AddTTHandiCraftsSwagger();

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IEmailSender, MailKitEmailSender>();
            services.Configure<EmailOptions>(Configuration.GetSection("EmailOptions"));

            services.AddLocalization(options => options.ResourcesPath = "Resources");


            services.Configure<IdentityOptions>(Configuration.GetSection("IdentityOptions"));

            services.AddCors(ConfigureCors);


            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/error-local-development");
                app.UseForwardedHeaders();
            }
            else
            {
                app.UseExceptionHandler("/error-production");
                app.UseForwardedHeaders();
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseSwaggerTTHandiCrafts();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(AllowedDomainsCorsPolicy);

            app.UseRequestLocalization();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa => { spa.Options.SourcePath = "ClientApp"; });
        }

        private void ConfigureCors(CorsOptions options)
        {
            options.AddPolicy(AllowedDomainsCorsPolicy,
                builder =>
                {
                    builder.WithOrigins(new[] {"http://localhost:3000"}).AllowAnyMethod().AllowAnyHeader()
                        .AllowCredentials();
                });
        }
    }
}