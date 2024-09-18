namespace ToDoList.Models.DTOs;

public class ToDoDTO
{
    public required string Name { get; set; }
    
    public string Description { get; set; }

    public ToDo GetModelToDo()
    {
        return new()
        {
            Name = Name,
            Description = Description
        };
    }
}