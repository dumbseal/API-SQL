using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prak.Models;

namespace Prak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        public PraktikaContext Context { get; }

        public AnswersController(PraktikaContext context)
        {
            Context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() // Получение всех записей
        {
            List<Answer> answers = await Context.Answers.ToListAsync();
            return Ok(answers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) // Получение одной записи
        {
            Answer? answer = await Context.Answers.FindAsync(id);
            if (answer == null)
            {
                return NotFound();
            }
            return Ok(answer);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Answer answer) // Создание одной записи
        {
            Context.Answers.Add(answer);
            await Context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = answer.UserIds }, answer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Answer answer) // Изменение существующей записи
        {
            if (id != answer.UserIds)
            {
                return BadRequest();
            }

            Context.Entry(answer).State = EntityState.Modified;
            await Context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) // Удаление существующей записи
        {
            Answer? answer = await Context.Answers.FindAsync(id);
            if (answer == null)
            {
                return NotFound();
            }

            Context.Answers.Remove(answer);
            await Context.SaveChangesAsync();

            return NoContent();
        }
    }
}