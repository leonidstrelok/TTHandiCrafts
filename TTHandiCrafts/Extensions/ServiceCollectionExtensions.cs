using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace TTHandiCrafts.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTTHandiCraftsRequestLocalization(this IServiceCollection services)
        {
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(context =>
                {
                    if (context.Request.Path.StartsWithSegments("/api"))
                    {
                        return new AcceptLanguageHeaderRequestCultureProvider().DetermineProviderCultureResult(context);
                    }

                    return new CookieRequestCultureProvider().DetermineProviderCultureResult(context);
                }));

                var tkCulture = new CultureInfo("tk");
                tkCulture.NumberFormat.NumberDecimalSeparator = ".";
                tkCulture.NumberFormat.CurrencyGroupSeparator = ".";
                
                var ruCulture = new CultureInfo("ru");
                ruCulture.NumberFormat.NumberDecimalSeparator = ".";
                ruCulture.NumberFormat.CurrencyGroupSeparator = ".";
                
                var enCulture = new CultureInfo("en");
                enCulture.NumberFormat.NumberDecimalSeparator = ".";
                enCulture.NumberFormat.CurrencyGroupSeparator = ".";

                var supportedCultures = new List<CultureInfo>
                {
                    tkCulture,
                    ruCulture,
                    enCulture
                };

                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.SetDefaultCulture("ru");
            });

            return services;
        }

        public static IServiceCollection AddTTHandiCraftsSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "TTHandiCrafts", Version = "v1"});
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.EnableAnnotations();
            });

            return services;
        }
    }
}