using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todo_dotnet.Dto;
using todo_dotnet.Models;
using todo_dotnet.Services;

namespace todo_dotnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoItemController : ControllerBase
{
    private readonly ITodoItemService _service;

    public TodoItemController(TodoContext context, ITodoItemService service)
    {
        _service = service;
    }

    [HttpGet("{id:long}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TodoItem>> Get(long id)
    {
        var item = await _service.Get(id);

        if (item is null)
        {
            return NotFound();
        }

        return Ok(item);
    }


    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TodoItem>> Create([FromBody] CreateTodoDto data)
    {
        var todo = await _service.Create(data);

        return CreatedAtAction(nameof(Get), new { id = todo.Id }, todo);
    }

    [HttpPut("{id:long}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TodoItem>> Update(long id, [FromBody] CreateTodoDto data)
    {
        var todo = await _service.Get(id);
        if (todo is null)
        {
            return NotFound();
        }

        await _service.Update(id, data);
        return Ok(todo);
    }
}
