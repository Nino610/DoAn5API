using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EclassCDCD.Models;
namespace EclassCDCD.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ClassController : ControllerBase
	{
        private readonly CoreDbContext _context;

        public ClassController(CoreDbContext context)
        {
            _context = context;
        }

        // GET: api/Subjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Classes>>> GetSubjects()
        {
            return await _context.Classes.ToListAsync();
        }

        // GET: api/Subjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Classes>> GetClass(string id)
        {
            var Classes = await _context.Classes.FindAsync(id);

            if (Classes == null)
            {
                return NotFound();
            }

            return Classes;
        }

        // PUT: api/Subjects/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Route("sua/{id}")]
        public async Task<IActionResult> PutClass(string id, Classes classs)
        {
            if (id != classs.ClassId)
            {
                return BadRequest();
            }

            _context.Entry(classs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassExists(id))
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

        // POST: api/Subjects
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Route("them")]
        public async Task<ActionResult<Classes>> PostClass(Classes classs)
        {
            _context.Classes.Add(classs);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClassesExists(classs.ClassId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetClass", new { id = classs.ClassId }, classs);
        }

		private bool ClassesExists(string classId)
		{
			throw new NotImplementedException();
		}

		// DELETE: api/Subjects/5
		[HttpDelete("{id}")]
        [Route("xoa/{id}")]
        public async Task<ActionResult<Classes>> DeleteClass(string id)
        {
            var classs = await _context.Classes.FindAsync(id);
            if (classs == null)
            {
                return NotFound();
            }

            _context.Classes.Remove(classs);
            await _context.SaveChangesAsync();

            return classs;
        }
        private bool ClassExists(string id)
        {
            return _context.Classes.Any(e => e.ClassId == id);
        }
    }
}
