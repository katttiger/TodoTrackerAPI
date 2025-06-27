using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data.Entity;
using TodoTrackerAPI.Contexts;
using TodoTrackerAPI.Models;
using TodoTrackerAPI.Requests;

namespace TodoTrackerAPI.Controllers
{
    public class TodoController : ControllerBase
    {
        public readonly TodoContext _context;
        public TodoController(TodoContext context) => _context = context;

        [HttpGet("GetTasks")]
        public async Task<IEnumerable<TodoItem>> GetTodoItems()
        {
            return await _context.Tasks.ToListAsync();
        }

        [HttpPost("AddTask")]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoRequest todoRequest)
        {
            TodoItem newTask = new TodoItem()
            {
                Task = todoRequest.Task,
            };

            if (newTask.Task.IsNullOrEmpty())
            {
                return BadRequest("Your task must have a name.");
            }
            _context.Add(newTask);
            await _context.SaveChangesAsync();
            return newTask;
        }

        [HttpPatch("UpdateStatusTask")]
        public async Task<ActionResult<TodoItem>> PatchStatusTodoItem(TodoIdRequest requestId)
        {
            var task=_context.Tasks.FirstOrDefault(t=>t.Id==requestId.Id);
            if (task == null)
            {
                return NotFound("Task does not exist or your id is invalid.");
            }
            else if (task.IsCompleted)
            {
                task.IsCompleted = false;
            }
            else
            {
                task.IsCompleted = true;
            }
            await _context.SaveChangesAsync();
                return task;            
        }

        [HttpDelete("DeleteTaskFromList")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem(TodoIdRequest requestId)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == requestId.Id);
            if (task == null)
            { return NotFound(); }
            else
            {
                _context.Remove(task);
            }
            await _context.SaveChangesAsync();
            return task;
        }
    }
}

