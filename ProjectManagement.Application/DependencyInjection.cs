using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ProjectManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                // Order matters: Logging outer wraps Validation inner wraps Handler
                // - LoggingBehavior logs request name + execution time (including validation time) and errors
                // - ValidationBehavior runs FluentValidation before handler and short-circuits on failure
                // This ensures validation failures are still logged and handler is never reached.
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(Behaviors.LoggingBehavior<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(Behaviors.ValidationBehavior<,>));
            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
