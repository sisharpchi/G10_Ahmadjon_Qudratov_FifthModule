using ToDoList.Bll.Dtos;
using ToDoList.Dal.Entities;

namespace ToDoList.Bll.Services;

public interface IToDoItemService
{
    Task<long> PostToDoItemAsync(ToDoListCreateDto toDoListCreateDto);
    Task DeleteToDoItemByIdAsync(long id);
    Task PutToDoItemAsync(ToDoListGetDto updatedToDoItem);
    Task<GetAllResponse> GetAllToDoItemsAsync(int skip, int take);
    Task<ToDoListGetDto> GetToDoItemByIdAsync(long id);
    Task<GetAllResponse> GetByDueDateAsync(DateTime dateTime);
    Task<GetAllResponse> GetCompletedAsync(int skip, int take);
    Task<GetAllResponse> GetIncompleteAsync(int skip, int take);
}