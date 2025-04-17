using AutoMapper;
using FluentValidation;
using ToDoList.Bll.Services;
using ToDoList.Repository.Services;

namespace ToDoList.Api.Configurations;

public static class DependencyInjectionConfiguration
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IToDoItemRepository, ToDoItemRepository>();
        services.AddScoped<IToDoItemService, ToDoItemService>();
    }
}
