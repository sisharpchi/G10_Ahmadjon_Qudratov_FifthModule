using AutoMapper;
using ToDoList.Bll.Dtos;
using ToDoList.Dal.Entities;

namespace ToDoList.Bll.Mappers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ToDoItem, ToDoListCreateDto>().ReverseMap();
        CreateMap<ToDoItem, ToDoListGetDto>().ReverseMap();
    }
}
