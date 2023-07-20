using todo_dotnet.Dto;
using todo_dotnet.Models;

namespace todo_dotnet.Services;

public interface ITodoItemService
{ 
    public Task<TodoItem?> Get(long id);
    public Task<TodoItem> Create(CreateTodoDto data);
    public Task<TodoItem> Update(long id, CreateTodoDto data);
}
