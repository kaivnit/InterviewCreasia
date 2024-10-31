using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Interview.Application.Validators;

public static class ValidatorExtension
{
    public static void AddValidators(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
