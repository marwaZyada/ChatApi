using Chat.Application.Behavior;
using Chat.Application.Contracts;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Chat.Application
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        


            // fluent validation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient( typeof(IPipelineBehavior<,>),typeof(ValidationBehavior<,>));

           


            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }

    }
}
