using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ToDoList.Models;

namespace ToDoList.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database; // Se almacena la instancia de la db

    public MongoDbContext(IOptions<DatabaseSettings> databaseSettings)
    {
        MongoClient mongoClient = new(databaseSettings.Value.ConnectionString); //Crea una instancia de mongoClient usando la cadena de conexion
        _database = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName); //Se obtiene la db usando el nombre de la base de datos
    }

    public IMongoCollection<ToDo> ToDos => _database.GetCollection<ToDo>("ToDos"); //Conexion con la collecion


}