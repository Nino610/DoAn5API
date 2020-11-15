using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EclassCDCD.Models;
using EclassCDCD.ViewModels;
using Newtonsoft.Json;

namespace EclassCDCD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly CoreDbContext _context;

        public AnswersController(CoreDbContext context)
        {
            _context = context;
        }

        // GET: api/Answers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Answers>>> GetAnswers()
        {
            return await _context.Answers.ToListAsync();
        }

        // GET: api/Answers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Answers>> GetAnswers(Guid id)
        {
            var answers = await _context.Answers.FindAsync(id);

            if (answers == null)
            {
                return NotFound();
            }

            return answers;
        }

        // PUT: api/Answers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnswers(Guid id, Answers answers)
        {
            if (id != answers.AnswerId)
            {
                return BadRequest();
            }

            _context.Entry(answers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnswersExists(id))
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

        // POST: api/Answers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<int>> PostAnswers(AnswerViewModel answerViewModel)
        {
            DateTime now = DateTime.Now;
            Guid guid = Guid.NewGuid();
            string[] values = JsonConvert.DeserializeObject<string[]>(answerViewModel.value);
            int[] quesids = JsonConvert.DeserializeObject<int[]>(answerViewModel.questionId);
            Answers answers = new Answers()
            {
                AnswerId = guid,
                StudentId = "181MNA001", // mặc định là ng này đăng nhập khảo sát
                PlanId = answerViewModel.PlanID,
                Time = now
            };
            _context.Answers.Add(answers);
            var res = await _context.SaveChangesAsync();
            for (int i = 0; i < quesids.Length; ++i)
            {
                AnswerDetails answerDetails = new AnswerDetails()
                {
                    AnswerId = answers.AnswerId,
                    QuestionId = quesids[i],
                    Value = values[i]
                };
                _context.AnswerDetails.Add(answerDetails);
                await _context.SaveChangesAsync();
            }
            return res;
        }

        // DELETE: api/Answers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Answers>> DeleteAnswers(Guid id)
        {
            var answers = await _context.Answers.FindAsync(id);
            if (answers == null)
            {
                return NotFound();
            }

            _context.Answers.Remove(answers);
            await _context.SaveChangesAsync();

            return answers;
        }

        private bool AnswersExists(Guid id)
        {
            return _context.Answers.Any(e => e.AnswerId == id);
        }
    }
}
