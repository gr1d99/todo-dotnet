using System.ComponentModel;

namespace todo_dotnet.Models;

public class TodoItem
{
    public long Id { get; set; }
    public string? Name { get; set; }
    [DefaultValue(false)]
    public bool IsComplete { get; set; }
}
