using ToDoList.Dal.Entities;

namespace ToDoList.Repository.Services;

public interface IToDoItemRepository
{
    Task<long> InsertToDoItemAsync(ToDoItem toDoItem);
    Task DeleteToDoItemByIdAsync(long id);
    Task UpdateToDoItemAsync(ToDoItem updatedToDoItem);
    Task<List<ToDoItem>> SelectAllToDoItemsAsync(int skip, int take);
    Task<ToDoItem> SelectToDoItemByIdAsync(long id);
    Task<List<ToDoItem>> SelectByDueDateAsync(DateTime dateTime);
    Task<List<ToDoItem>> SelectCompletedAsync(int skip, int take);
    Task<List<ToDoItem>> SelectIncompleteAsync(int skip, int take);
    int SelectToDoListCount();
}