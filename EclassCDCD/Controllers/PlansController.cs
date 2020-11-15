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
    public class PlansController : ControllerBase
    {
        private readonly CoreDbContext _context;

        public PlansController(CoreDbContext context)
        {
            _context = context;
        }

        // GET: api/Plans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plans>>> GetPlans()
        {
            return await _context.Plans.Include(x => x.Subject).Include(x => x.Employee).Include(x=>x.Class).ToListAsync();
        }

        // GET: api/Plans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Plans>> GetPlans(int id)
        {
            var plan = from t1 in _context.Plans.Include(x => x.Subject).Include(x => x.Employee)
                       where t1.PlanId == id
                       select t1;
            return Ok(plan.First());
        }

        // PUT: api/Plans/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlans(int id, Plans plans)
        {
            if (id != plans.PlanId)
            {
                return BadRequest();
            }

            _context.Entry(plans).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlansExists(id))
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

        // POST: api/Plans
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Plans>> PostPlans(Plans plans)
        {
            _context.Plans.Add(plans);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlans", new { id = plans.PlanId }, plans);
        }

        // DELETE: api/Plans/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Plans>> DeletePlans(int id)
        {
            var plans = await _context.Plans.FindAsync(id);
            if (plans == null)
            {
                return NotFound();
            }

            _context.Plans.Remove(plans);
            await _context.SaveChangesAsync();

            return plans;
        }

        private bool PlansExists(int id)
        {
            return _context.Plans.Any(e => e.PlanId == id);
        }
    }
}
