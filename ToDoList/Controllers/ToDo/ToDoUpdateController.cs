using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Models.DTOs;
using ToDoList.Repository.ToDo;

namespace ToDoList.Controllers.ToDo;

[Route("api/[controller]")]
[ApiController]
public class ToDoUpdateController : ControllerBase
{
    private readonly IToDoRepository _toDoRepository;

    public ToDoUpdateController(IToDoRepository toDoRepository)
    {
        _toDoRepository = toDoRepository;
    }

    [HttpPut("[action]/{id}")]
    public async Task<IActionResult> UpdateToDo(string id, ToDoDTO toDoDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _toDoRepository.UpdateToDo(id, toDoDto);

        if (!result.status)
        {
            return NotFound(new
            {
                status = false,
                response = result.response
            });
        }

        return Ok(new
        {
            status = true,
            response = result.response
        });
    }

    [HttpPut("[action]/{id}")]
    public async Task<IActionResult> UpdateStatusToDo([FromBody] ToDoStatus toDoStatus, string id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _toDoRepository.UpdateStatusToDo(id, toDoStatus);

        if (!result.status)
        {
            return NotFound(new
            {
                status = false,
                response = result.response
            });
        }

        return Ok(new
        {
            status = true,
            response = result.response
        });
    }

    [HttpPut("[action]/{id}")]
    public async Task<IActionResult> SoftDeleteToDo(string id)
    {
        var result = await _toDoRepository.SoftDeleteToDo(id);

        if (!result.status)
        {
            return NotFound(result);
        }
        
        return Ok(new
        {
            status = true,
            response = result.response
        });
    }
}