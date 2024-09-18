using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ToDoList.Models;

public class ToDo
{
    [BsonId] //Permite que el id se auto genere
    [BsonRepresentation(BsonType.ObjectId)] //Refiere a esta propiedad como el id
    public string? Id { get; set; }
    
    public required string Name { get; set; }
    
    public string? Description { get; set; }

    public DateOnly Date_Create { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    public DateOnly? Date_Finish { get; set; } = null;

    [JsonConverter(typeof(JsonStringEnumConverter))] //Ayuda a serealizar el enum de numero a cadena
    public ToDoStatus Status { get; set; } = ToDoStatus.Active;

    public bool IsActive { get; set; } = true;
    
}

public enum ToDoStatus
{
    Finish,
    Progress,
    Earring,
    Active
}

