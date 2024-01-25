using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prak.Models;

namespace Prak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public PraktikaContext Context { get; }

        public UsersController(PraktikaContext context)
        {
            Context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() // Получение всех записей
        {
            List<User> users = await Context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) // Получение одной записи
        {
            User? user = await Context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] User user) // Создание одной записи
        {
            Context.Users.Add(user);
            await Context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = user.UserIds }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] User user) // Изменение существующей записи
        {
            if (id != user.UserIds)
            {
                return BadRequest();
            }

            Context.Entry(user).State = EntityState.Modified;
            await Context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) // Удаление существующей записи
        {
            User? user = await Context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            Context.Users.Remove(user);
            await Context.SaveChangesAsync();

            return NoContent();
        }
    }
}