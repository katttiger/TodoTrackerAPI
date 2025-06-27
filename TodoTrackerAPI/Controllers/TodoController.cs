using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data.Entity;
using TodoTrackerAPI.Contexts;
using TodoTrackerAPI.Models;
using TodoTrackerAPI.Requests;

namespace TodoTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        public readonly TodoContext _context;
        public TodoController(TodoContext context) => _context = context;

        [HttpGet("GetTasks")]
        public async Task<IEnumerable<TodoItem>> GetTodoItems()=>await _context.Tasks.ToListAsync();

        [HttpPost("AddTask")]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoRequest todoRequest)
        {
            TodoItem newTask=new TodoItem()
            {
                Task=todoRequest.Task,
            };

            if (newTask.Task.IsNullOrEmpty())
            {
                return BadRequest("Your task must have a name.");
            }
            _context.Add(newTask);
            await _context.SaveChangesAsync();
            return newTask;
        }
}
}

