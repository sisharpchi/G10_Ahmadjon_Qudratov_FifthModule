using ToDoList.Bll.Mappers;

namespace ToDoList.Api.Configurations;

public static class MappersConfigurations
{
    public static void ConfigureAutoMappers(this IServiceCollection builder)
    {
        builder.AddAutoMapper(typeof(MappingProfiles));
    }
}
