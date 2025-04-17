using FluentValidation;
using ToDoList.Bll.Validators;

namespace ToDoList.Api.Configurations;

public static class FluentValidatorsDependecyI
{
    public static void AddFluentValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<ToDoListCreateDToValidator>();
    }
}
