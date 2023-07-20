using Microsoft.EntityFrameworkCore;
using todo_dotnet.Dto;
using todo_dotnet.Models;

namespace todo_dotnet.Services;

public class TodoItemService : ITodoItemService
{
    private readonly TodoContext _context;

    public TodoItemService(TodoContext context)
    {
        _context = context;
    }

    public async Task<TodoItem?> Get(long id)
    {
        return await _context.TodoItems.Where(t => t.Id == id).FirstOrDefaultAsync();
    }

    public async Task<TodoItem> Create(CreateTodoDto data)
    {
        var todo = new TodoItem()
        {
            Name = data.Name,
            IsComplete = data.IsCompleted
        };
        
        _context.TodoItems.Add(todo);

        await _context.SaveChangesAsync();

        return todo;
    }

    public async Task<TodoItem> Update(long id, CreateTodoDto data)
    {
        var todo = await _context.TodoItems.Where(t => t.Id == id).FirstAsync();
        todo.Name = data.Name;
        todo.IsComplete = data.IsCompleted;
        _context.Entry(todo).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return todo;
    }
}
