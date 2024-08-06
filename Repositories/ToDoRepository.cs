using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Data;
using ToDoApp.Models;

namespace ToDoApp.Repositories
{
    public class ToDoRepository
    {
        private readonly IMongoCollection<ToDoItem> _toDoItems;

        public ToDoRepository()
        {
            var context = new MongoDBContext();
            _toDoItems = context.ToDoItems;
        }

        public async Task<List<ToDoItem>> GetAllAsync()
        {
            return await _toDoItems.Find(toDo => true).ToListAsync();
        }

        public async Task<ToDoItem> GetByIdAsync(string id)
        {
            return await _toDoItems.Find<ToDoItem>(toDo => toDo.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(ToDoItem toDoItem)
        {
            await _toDoItems.InsertOneAsync(toDoItem);
        }

        public async Task UpdateAsync(string id, ToDoItem toDoItem)
        {
            await _toDoItems.ReplaceOneAsync(toDo => toDo.Id == id, toDoItem);
        }

        public async Task DeleteAsync(string id)
        {
            await _toDoItems.DeleteOneAsync(toDo => toDo.Id == id);
        }
    }
}
