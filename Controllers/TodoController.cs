using Microsoft.AspNetCore.Mvc;
using PollyWithHttpClientFactory.Services;

namespace PollyWithHttpClientFactory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet("GetTodo/{id}")]
        public async Task<ActionResult> GetTodo(int id)
        {
            return Ok(await _todoService.GetTodo(id));
        }
    }
}
