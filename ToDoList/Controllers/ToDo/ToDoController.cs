using Microsoft.AspNetCore.Mvc;
using ToDoList.Repository.ToDo;

namespace ToDoList.Controllers.ToDo;

[Route("api/[controller]")]
[ApiController]
public class ToDoController : ControllerBase
{
    private readonly IToDoRepository _toDoRepository;

    public ToDoController(IToDoRepository toDoRepository)
    {
        _toDoRepository = toDoRepository;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllToDos()
    {
        var result = await _toDoRepository.GetAllToDos();

        return Ok(new
        {
            status = true,
            response = result
        });
    }

    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> GetToDoById(string id)
    {
        var result = await _toDoRepository.GetToDoById(id);
        
        return Ok(new
        {
            status = true,
            response = result
        });
    }
}