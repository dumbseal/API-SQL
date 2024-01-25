using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prak.Models;

namespace Prak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        public PraktikaContext Context { get; }

        public CommentsController(PraktikaContext context)
        {
            Context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() // Получение всех записей
        {
            List<Comment> comments = await Context.Comments.ToListAsync();
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) // Получение одной записи
        {
            Comment? comment = await Context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Comment comment) // Создание одной записи
        {
            Context.Comments.Add(comment);
            await Context.SaveChangesAsync();
            // Исправленное использование nameof(GetById) для создания URL.
            return CreatedAtAction(nameof(GetById), new { id = comment.CommentId }, comment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Comment comment) // Изменение существующей записи
        {
            if (id != comment.CommentId)
            {
                return BadRequest();
            }

            Context.Entry(comment).State = EntityState.Modified;
            await Context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) // Удаление существующей записи
        {
            Comment? comment = await Context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            Context.Comments.Remove(comment);
            await Context.SaveChangesAsync();

            return NoContent();
        }
    }
}