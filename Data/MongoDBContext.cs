using MongoDB.Driver;
using ToDoApp.Models;

namespace ToDoApp.Data 

{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            _database = client.GetDatabase("ToDoAppDB");
        }

        public IMongoCollection<ToDoItem> ToDoItems => _database.GetCollection<ToDoItem>("ToDoItems");
    }
}