using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ToDoList.Models;

namespace ToDoList.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database; // Se almacena la instancia de la db

    public MongoDbContext(IOptions<DatabaseSettings> databaseSettings)
    {
        var settings = MongoClientSettings.FromUrl(new
            MongoUrl(databaseSettings.Value.ConnectionString)
        );

        settings.SslSettings = new SslSettings
        {
            CheckCertificateRevocation = false
        };
        
        MongoClient mongoClient = new(settings); //Crea una instancia de mongoClient usando la cadena de conexion
        
        _database = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName); //Se obtiene la db usando el nombre de la base de datos
    }

    public IMongoCollection<ToDo> ToDos => _database.GetCollection<ToDo>("ToDos"); //Conexion con la collecion


}