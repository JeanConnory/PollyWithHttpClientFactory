using PollyWithHttpClientFactory.Models;

namespace PollyWithHttpClientFactory.Services
{
    public interface ITodoService
    {
        Task<TodoModel> GetTodo(int id);
    }
}
