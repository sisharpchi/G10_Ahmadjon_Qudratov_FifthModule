namespace ToDoList.Bll.Dtos;

public class GetAllResponse
{
    public long Count { get; set; }
    public List<ToDoListGetDto> Dtos { get; set; }
}
