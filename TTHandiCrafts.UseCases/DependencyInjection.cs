using System.Reflection;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using TTHandiCrafts.UseCases.Commons.Behaviours;

namespace TTHandiCrafts.UseCases
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddTTHandiCraftsUseCases(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestPostProcessorBehavior<,>));
            services.AddScoped(typeof(IRequestPreProcessor<>), typeof(LoggingBehaviour<>));


            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}