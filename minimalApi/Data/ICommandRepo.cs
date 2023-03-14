//Adding a layer of abstraction - Define the interface and what it can provide to a consumer
using minimalApi.Models;

namespace minimalApi.Data
{
    public interface ICommandRepo
    {
        //Defining an async method that return a task - is a method signature
        Task SaveChanges();
        Task<Command?> GetCommandById(int id);
        Task<IEnumerable<Command>> GetAllCommands();
        Task CreateCommand(Command cmd);
        void DeleteCommand(Command cmd);
    }
}
