using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todo_dotnet.Models;

namespace todo_dotnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoItemController : ControllerBase
{
    private TodoContext _context;

    public TodoItemController(TodoContext context)
    {
        _context = context;
    }

    [HttpGet("{id:long}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TodoItem>> Get(long id)
    {
        var item = await _context.TodoItems.Where(t => t.Id == id).FirstOrDefaultAsync();

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
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TodoItem>> Create(TodoItem data)
    {
        try
        {
            _context.TodoItems.Add(data);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = data.Id }, data);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

            return BadRequest(e);
        }
    }
}