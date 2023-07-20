using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace todo_dotnet.Dto;

public class CreateTodoDto
{
    [Required]
    public string Name { get; set; } = String.Empty;
    [DefaultValue(false)]
    public bool IsCompleted { get; set; }
}
