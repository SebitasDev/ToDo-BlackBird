using MongoDB.Driver;
using ToDoList.Data;
using ToDoList.Models;
using ToDoList.Models.DTOs;

namespace ToDoList.Repository.ToDo;

public class ToDoRepository : IToDoRepository
{
    private readonly MongoDbContext _context;

    public ToDoRepository(MongoDbContext context)
    {
        _context = context;
    }
    
    //GET
    public async Task<List<Models.ToDo>> GetAllToDos()
    {
        try
        {
            List<Models.ToDo> listToDo = await _context.ToDos.Find(t => true)
                .ToListAsync();

            return listToDo;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Models.ToDo> GetToDoById(string id)
    {
        try
        {
            Models.ToDo toDo = await _context.ToDos.Find(t => t.Id == id)
                .SingleOrDefaultAsync();

            return toDo;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    //CREATE
    public async Task<Models.ToDo> CreateToDo(ToDoDTO toDoDto)
    {
        try
        {
            Models.ToDo newTodo = toDoDto.GetModelToDo();
            await _context.ToDos.InsertOneAsync(newTodo);

            return newTodo;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    //UPDATE
    public async Task<(string response, bool status)> UpdateToDo(string id, ToDoDTO toDoDto)
    {
        try
        {
            var toDoUpdate = Builders<Models.ToDo>.Update
                .Set(t => t.Name, toDoDto.Name)
                .Set(t => t.Description, toDoDto.Description);

            var result = await _context.ToDos.UpdateOneAsync(p => p.Id == id, toDoUpdate);
            
            if (result.ModifiedCount == 0)
            {
                return (response: "ToDo not found", status: false);
            }

            return (response: "Update finish", status: true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<(string response, bool status)> UpdateStatusToDo(string id, ToDoStatus newStatus)
    {
        try
        {
            var statusToDoUpdate = Builders<Models.ToDo>.Update
                .Set(t => t.Status, newStatus);

            var result = await _context.ToDos.UpdateOneAsync(t => t.Id == id, statusToDoUpdate);
            
            if (result.ModifiedCount == 0)
            {
                return (response: "ToDo not found", status: false);
            }

            return (response: "Status update finish", status: true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<(string response, bool status)> SoftDeleteToDo(string id)
    {
        try
        {
            var toDoSoftDelete = Builders<Models.ToDo>.Update
                .Set(t => t.IsActive, false);

            var result = await _context.ToDos.UpdateOneAsync(t => t.Id == id, toDoSoftDelete);

            if (result.ModifiedCount == 0)
            {
                return (response: "ToDo not found", status: false);
            }

            return (response: "ToDo delete success", status: true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}