using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prak.Models;

namespace Prak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        public PraktikaContext Context { get; }


        public ArticlesController(PraktikaContext context)
        {
            Context = context;
        }

        // Метод для получения всех статей из базы данных.
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Article> articles = await Context.Articles.ToListAsync();
            return Ok(articles);
        }

        // Метод для получения статьи по идентификатору.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Article? article = await Context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            return Ok(article);
        }

        // Метод для добавления новой статьи в базу данных.
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Article article)
        {
            Context.Articles.Add(article);
            await Context.SaveChangesAsync();
            // Исправленное использование nameof(GetById) для создания URL.
            return CreatedAtAction(nameof(GetById), new { id = article.ArticleId }, article);
        }

        // Метод для обновления существующей статьи в базе данных.
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Article article)
        {
            if (id != article.ArticleId)
            {
                return BadRequest();
            }

            Context.Entry(article).State = EntityState.Modified;
            await Context.SaveChangesAsync();

            return NoContent();
        }

        // Метод для удаления статьи из базы данных.
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Article? article = await Context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            Context.Articles.Remove(article);
            await Context.SaveChangesAsync();

            return NoContent();
        }
    }
}