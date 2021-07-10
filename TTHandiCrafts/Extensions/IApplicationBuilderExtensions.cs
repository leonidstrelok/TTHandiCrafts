using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace TTHandiCrafts.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        public static void UseSwaggerTTHandiCrafts(this IApplicationBuilder app)
        {
            SwaggerBuilderExtensions.UseSwagger(app);

            app.UseSwaggerUI(config =>
            {
                config.DefaultModelsExpandDepth(-1);
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "TTHandiCrafts API v1");
                config.RoutePrefix = "swagger";
                config.DocExpansion(DocExpansion.None);
                config.EnableFilter();
              
            });
        }
    }
}