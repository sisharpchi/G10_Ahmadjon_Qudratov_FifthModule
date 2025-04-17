using AutoMapper;
using FluentValidation;
using ToDoList.Bll.Dtos;
using ToDoList.Dal.Entities;
using ToDoList.Repository.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ToDoList.Bll.Services;

public class ToDoItemService : IToDoItemService
{
    private readonly IToDoItemRepository _toDoItemRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<ToDoListCreateDto> _validator;
    public ToDoItemService(IToDoItemRepository toDoListRepository, IMapper mapper, IValidator<ToDoListCreateDto> validator)
    {
        _toDoItemRepository = toDoListRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task DeleteToDoItemByIdAsync(long id)
    {
        await _toDoItemRepository.DeleteToDoItemByIdAsync(id);
    }

    public async Task<GetAllResponse> GetAllToDoItemsAsync(int skip, int take)
    {
        var count = _toDoItemRepository.SelectToDoListCount();
        var res = await _toDoItemRepository.SelectAllToDoItemsAsync(skip, take);
        return new GetAllResponse() { Count = count, Dtos = res.Select(x => _mapper.Map<ToDoListGetDto>(x)).ToList()};
    }

    public async Task<GetAllResponse> GetByDueDateAsync(DateTime dateTime)
    {
        var count = _toDoItemRepository.SelectToDoListCount();
        var toDoItems = await _toDoItemRepository.SelectByDueDateAsync(dateTime);
        return new GetAllResponse() { Count = count, Dtos = toDoItems.Select(x => _mapper.Map<ToDoListGetDto>(x)).ToList()};
    }

    public async Task<GetAllResponse> GetCompletedAsync(int skip, int take)
    {
        var count = _toDoItemRepository.SelectToDoListCount();
        var toDoItems = await _toDoItemRepository.SelectCompletedAsync(skip, take);
        return new GetAllResponse() { Count = count, Dtos = toDoItems.Select(x => _mapper.Map<ToDoListGetDto>(x)).ToList() };
    }

    public async Task<GetAllResponse> GetIncompleteAsync(int skip, int take)
    {
        var count = _toDoItemRepository.SelectToDoListCount();
        var toDoItems = await _toDoItemRepository.SelectIncompleteAsync(skip, take);
        return new GetAllResponse() { Count = count, Dtos = toDoItems.Select(x => _mapper.Map<ToDoListGetDto>(x)).ToList() };
    }

    public async Task<ToDoListGetDto> GetToDoItemByIdAsync(long id)
    {
        return _mapper.Map<ToDoListGetDto>(await _toDoItemRepository.SelectToDoItemByIdAsync(id));
    }

    public async Task<long> PostToDoItemAsync(ToDoListCreateDto toDoListCreateDto)
    {
        var status = _validator.Validate(toDoListCreateDto);
        if (!status.IsValid)
        {
            throw new Exception("toDoList is not Valid");
        }
        var entity = _mapper.Map<ToDoItem>(toDoListCreateDto);
        entity.CreatedAt = DateTime.Now;
        entity.IsCompleted = false;
        return await _toDoItemRepository.InsertToDoItemAsync(entity);
    }

    public async Task PutToDoItemAsync(ToDoListGetDto updatedToDoItem)
    {
        await _toDoItemRepository.UpdateToDoItemAsync(_mapper.Map<ToDoItem>(updatedToDoItem));
    }
}
