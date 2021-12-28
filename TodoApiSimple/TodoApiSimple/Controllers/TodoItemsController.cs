using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApiSimple.Models;

namespace TodoApiSimple.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TodoItemsController(AppDbContext context)
        {
            _context = context;
        }

        // get: api/todoitems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> gettodoitems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        // get: api/todoitems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> gettodoitem(int id)
        {
            var todoitem = await _context.TodoItems.FindAsync(id);

            if (todoitem == null)
            {
                return NotFound();
            }

            return todoitem;
        }

        // post: api/todoitems
        [HttpPost]
        public async Task<ActionResult<TodoItem>> posttodoitem(TodoItem todoitem)
        {
            await _context.TodoItems.AddAsync(todoitem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(gettodoitems), new { id = todoitem.Id }, todoitem);
        }

        // put: api/todoitems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> puttodoitem(int id, TodoItem todoitem)
        {
            if (id != todoitem.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoitem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // delete: api/todoitems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> deletetodoitem(int id)
        {
            var todoitem = await _context.TodoItems.FindAsync(id);
            if (todoitem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoitem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExists(int id) =>
             _context.TodoItems.Any(e => e.Id == id);
    }
}
