using System;
using System.IdentityModel.Tokens.Jwt;
using IdentityModel;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TTHandiCrafts.UseCases.Commons.Constants;

namespace TTHandiCrafts.Infrastructure.Identities.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddIdentityServer4(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityServer()
                .AddApiAuthorization<IdentityUser, TTHandiCraftsIdentityDbContext>(options =>
                    ConfigureApiAuthorization(options, configuration))
                .AddProfileService<DefaultProfileService>();

            var tokenValidIssuers = configuration.GetSection("IdentityServer:TokenValidationParameters:ValidIssuers")
                .Get<string[]>();

            services.Configure<JwtBearerOptions>(IdentityServerJwtConstants.IdentityServerJwtBearerScheme,
                options => { options.TokenValidationParameters.ValidIssuers = tokenValidIssuers; });

            services.AddAuthentication(IdentityServerJwtConstants.IdentityServerJwtBearerScheme)
                .AddIdentityServerJwt();
        }

        private static void ConfigureApiAuthorization(ApiAuthorizationOptions options, IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            ConfigureClients(options.Clients,configuration);

            ConfigureApiResources(options.ApiResources);
        }

        private static void ConfigureApiResources(ApiResourceCollection apiResources)
        {
            foreach (var apiResource in apiResources)
                apiResource.UserClaims = new[]
                {
                    EmployeeClaimTypes.UserIdentifier, EmployeeClaimTypes.FullName, JwtClaimTypes.Role,
                    EmployeeClaimTypes.RoleId
                };
        }

        private static void ConfigureClients(ClientCollection clients, IConfiguration configuration)
        {
            foreach (var client in clients)
            {
                client.AllowOfflineAccess = true;
                client.AccessTokenLifetime = (int) TimeSpan.FromHours(8).TotalSeconds;
            }
            
        }
    }
}