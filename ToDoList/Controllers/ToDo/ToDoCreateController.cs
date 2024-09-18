using Microsoft.AspNetCore.Mvc;
using ToDoList.Models.DTOs;
using ToDoList.Repository.ToDo;

namespace ToDoList.Controllers.ToDo;

[Route("api/[controller]")]
[ApiController]
public class ToDoCreateController : ControllerBase
{
    private readonly IToDoRepository _toDoRepository;

    public ToDoCreateController(IToDoRepository toDoRepository)
    {
        _toDoRepository = toDoRepository;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> CreateToDo([FromBody] ToDoDTO toDoDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _toDoRepository.CreateToDo(toDoDto);

        return StatusCode(StatusCodes.Status201Created, new
        {
            status = true,
            response = result
        });
    }
}