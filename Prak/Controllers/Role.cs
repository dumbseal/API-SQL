using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prak.Models;

namespace Prak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        public PraktikaContext Context { get; }

        public RolesController(PraktikaContext context)
        {
            Context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() // Получение всех записей
        {
            List<Role> roles = await Context.Roles.ToListAsync();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) // Получение одной записи
        {
            Role? role = await Context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Role role) // Создание одной записи
        {
            Context.Roles.Add(role);
            await Context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = role.RoleLvl }, role);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Role role) // Изменение существующей записи
        {
            if (id != role.RoleLvl)
            {
                return BadRequest();
            }

            Context.Entry(role).State = EntityState.Modified;
            await Context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) // Удаление существующей записи
        {
            Role? role = await Context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            Context.Roles.Remove(role);
            await Context.SaveChangesAsync();

            return NoContent();
        }
    }
}