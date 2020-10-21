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
    public class FacultiesController : ControllerBase
    {
        private readonly CoreDbContext _context;

        public FacultiesController(CoreDbContext context)
        {
            _context = context;
        }

        // GET: api/Faculties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Faculties>>> GetFaculties()
        {
            return await _context.Faculties.ToListAsync();
        }

        // GET: api/Faculties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Faculties>> GetFaculties(string id)
        {
            var faculties = await _context.Faculties.FindAsync(id);

            if (faculties == null)
            {
                return NotFound();
            }

            return faculties;
        }

        // PUT: api/Faculties/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFaculties(string id, Faculties faculties)
        {
            if (id != faculties.FacultyId)
            {
                return BadRequest();
            }

            _context.Entry(faculties).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacultiesExists(id))
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

        // POST: api/Faculties
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Faculties>> PostFaculties(Faculties faculties)
        {
            _context.Faculties.Add(faculties);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FacultiesExists(faculties.FacultyId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFaculties", new { id = faculties.FacultyId }, faculties);
        }

        // DELETE: api/Faculties/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Faculties>> DeleteFaculties(string id)
        {
            var faculties = await _context.Faculties.FindAsync(id);
            if (faculties == null)
            {
                return NotFound();
            }

            _context.Faculties.Remove(faculties);
            await _context.SaveChangesAsync();

            return faculties;
        }

        private bool FacultiesExists(string id)
        {
            return _context.Faculties.Any(e => e.FacultyId == id);
        }
    }
}
