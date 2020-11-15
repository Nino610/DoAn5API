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
    public class QuestionsController : ControllerBase
    {
        private readonly CoreDbContext _context;

        public QuestionsController(CoreDbContext context)
        {
            _context = context;
        }

        // GET: api/Questions
        [HttpGet("Sv")]
        public async Task<List<Questions>> GetQuestionsSv()
        {
            var questions = await _context.Questions.Include(x => x.Options).Where(x => x.CateId == "SV").ToListAsync();
            return questions;
        }
        [HttpGet("Gv")]
        public async Task<ActionResult<IEnumerable<Questions>>> GetQuestionsGv()
        {
            var questions = await _context.Questions.Include(x => x.Options).Where(x => x.CateId == "GV").ToListAsync();
            return questions;
        }

        // GET: api/Questions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Questions>> GetQuestions(int id)
        {
            var questions = await _context.Questions.FindAsync(id);

            if (questions == null)
            {
                return NotFound();
            }

            return questions;
        }

        // PUT: api/Questions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestions(int id, Questions questions)
        {
            if (id != questions.QuestionId)
            {
                return BadRequest();
            }

            _context.Entry(questions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionsExists(id))
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

        // POST: api/Questions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Questions>> PostQuestions(Questions questions)
        {
            _context.Questions.Add(questions);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (QuestionsExists(questions.QuestionId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetQuestions", new { id = questions.QuestionId }, questions);
        }

        // DELETE: api/Questions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Questions>> DeleteQuestions(int id)
        {
            var questions = await _context.Questions.FindAsync(id);
            if (questions == null)
            {
                return NotFound();
            }

            _context.Questions.Remove(questions);
            await _context.SaveChangesAsync();

            return questions;
        }

        private bool QuestionsExists(int id)
        {
            return _context.Questions.Any(e => e.QuestionId == id);
        }
    }
}
