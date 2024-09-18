using ToDoList.Models;
using ToDoList.Models.DTOs;

namespace ToDoList.Repository.ToDo;

public interface IToDoRepository
{
    //GET
    Task<List<Models.ToDo>> GetAllToDos(); //Get all
    Task<Models.ToDo> GetToDoById(string id); //Get ById
    
    //CREATE
    Task<Models.ToDo> CreateToDo(ToDoDTO toDoDto); //Create ToDo
    
    //UPDATE
    Task<(string response, bool status)> UpdateToDo(string id, ToDoDTO toDoDto); //Update ToDo
    Task<(string response, bool status)> UpdateStatusToDo(string id, ToDoStatus newStatus); //Update StatusToDo
    Task<(string response, bool status)> SoftDeleteToDo(string id);

}