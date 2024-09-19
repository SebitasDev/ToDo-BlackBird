using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ToDoList.Models;

namespace ToDoList.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database; // Se almacena la instancia de la db

    public MongoDbContext(IOptions<DatabaseSettings> databaseSettings)
    {
        var settings = MongoClientSettings.FromUrl(new MongoUrl(databaseSettings.Value.ConnectionString));

        //Configura las opciones de SSL
        settings.SslSettings = new SslSettings 
        {
            CheckCertificateRevocation = false, //Deshabilita la verificacion de revocacion de certificado
            EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 //Habilita el protocolo TLS 1.2
        };

        // Establecer tiempo de espera de conexión y socket
        settings.ConnectTimeout = TimeSpan.FromSeconds(30);
        settings.SocketTimeout = TimeSpan.FromSeconds(30);

        MongoClient mongoClient = new MongoClient(settings);
        _database = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
    }

    public IMongoCollection<ToDo> ToDos => _database.GetCollection<ToDo>("ToDos"); //Conexion con la collecion


}