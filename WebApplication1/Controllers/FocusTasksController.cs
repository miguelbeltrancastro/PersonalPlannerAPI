using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FocusTasksController : ControllerBase
    {
        private readonly PersonalPlannerContext _context;

        public FocusTasksController(PersonalPlannerContext context)
        {
            _context = context;
        }

        // GET: api/FocusTasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FocusTask>>> GetFocusTasks()
        {
          if (_context.FocusTasks == null)
          {
              return NotFound();
          }
            return await _context.FocusTasks.ToListAsync();
        }

        // GET: api/FocusTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FocusTask>> GetFocusTask(long id)
        {
          if (_context.FocusTasks == null)
          {
              return NotFound();
          }
            var focusTask = await _context.FocusTasks.FindAsync(id);

            if (focusTask == null)
            {
                return NotFound();
            }

            return focusTask;
        }

        // PUT: api/FocusTasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFocusTask(long id, FocusTask focusTask)
        {
            if (id != focusTask.Id)
            {
                return BadRequest();
            }

            _context.Entry(focusTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FocusTaskExists(id))
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

        // POST: api/FocusTasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FocusTask>> PostFocusTask(FocusTask focusTask)
        {
          if (_context.FocusTasks == null)
          {
              return Problem("Entity set 'TodoContext.FocusTasks'  is null.");
          }
            _context.FocusTasks.Add(focusTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFocusTask), new { id = focusTask.Id }, focusTask);
        }

        // DELETE: api/FocusTasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFocusTask(long id)
        {
            if (_context.FocusTasks == null)
            {
                return NotFound();
            }
            var focusTask = await _context.FocusTasks.FindAsync(id);
            if (focusTask == null)
            {
                return NotFound();
            }

            _context.FocusTasks.Remove(focusTask);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FocusTaskExists(long id)
        {
            return (_context.FocusTasks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
