using Microsoft.AspNetCore.Mvc;
using ToDoList.Bll.Dtos;
using ToDoList.Bll.Services;

namespace ToDoList.Api.Controllers;

[Route("api/todo")]
[ApiController]

public class ToDoController : ControllerBase
{
    private readonly IToDoItemService _toDoItemService;

    public ToDoController(IToDoItemService service)
    {
        _toDoItemService = service;
    }

    [HttpPost("post")]
    public Task<long> Post([FromBody] ToDoListCreateDto dto)
    {
        return _toDoItemService.PostToDoItemAsync(dto);
    }

    [HttpGet("getById")]
    public Task<ToDoListGetDto> GetById(long id)
    {
        return _toDoItemService.GetToDoItemByIdAsync(id);
    }

    [HttpGet("getAll")]
    public Task<GetAllResponse> GetAll(int skip, int take)
    {
        return _toDoItemService.GetAllToDoItemsAsync(skip, take);
    }
    [HttpGet("incomplete")]
    public Task<GetAllResponse> GetIncomplete(int skip, int take)
    {
        return _toDoItemService.GetIncompleteAsync(skip, take);
    }
    [HttpGet("completed")]
    public Task<GetAllResponse> GetCompleted(int skip, int take)
    {
        return _toDoItemService.GetCompletedAsync(skip, take);
    }
    [HttpGet("DueDate")]
    public Task<GetAllResponse> GetByDueDate(DateTime data)
    {
        return _toDoItemService.GetByDueDateAsync(data);
    }

    [HttpPut("put")]
    public Task Put(ToDoListGetDto dto)
    {
        return _toDoItemService.PutToDoItemAsync(dto);
    }

    [HttpDelete("delete")]
    public Task Delete(long id)
    {
        return _toDoItemService.DeleteToDoItemByIdAsync(id);
    }
}
