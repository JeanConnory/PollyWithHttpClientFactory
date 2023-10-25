using PollyWithHttpClientFactory.Configuration;
using PollyWithHttpClientFactory.Models;

namespace PollyWithHttpClientFactory.Services
{
    public class TodoService : ITodoService
    {
        private readonly IApiConfig _config;
        private readonly HttpClient _httpClient;

        public TodoService(IApiConfig config, HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
        }

        public async Task<TodoModel> GetTodo(int id)
        {
            return await _httpClient.GetFromJsonAsync<TodoModel>($"{_config.BaseUrl}/todos/{id}");
        }
    }
}
