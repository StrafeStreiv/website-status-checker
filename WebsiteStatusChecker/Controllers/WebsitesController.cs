using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteStatusChecker.Data;
using WebsiteStatusChecker.Models;

namespace WebsiteStatusChecker.Controllers
{
    // Важно: Этот атрибут указывает, что это API-контроллер
    [ApiController]
    // Маршрут к контроллеру: /api/Websites
    [Route("api/[controller]")]
    public class WebsitesController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Внедряем зависимость AppDbContext через конструктор
        public WebsitesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Websites
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Website>>> GetWebsites()
        {
            // Возвращаем все сайты из базы
            return await _context.Websites.ToListAsync();
        }

        // GET: api/Websites/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Website>> GetWebsite(int id)
        {
            // Ищем сайт по ID
            var website = await _context.Websites.FindAsync(id);

            if (website == null)
            {
                return NotFound(); // Возвращаем 404 если не найден
            }

            return website;
        }

        // POST: api/Websites
        [HttpPost]
        public async Task<ActionResult<Website>> PostWebsite(Website website)
        {
            // Добавляем новый сайт в базу
            _context.Websites.Add(website);
            await _context.SaveChangesAsync();

            // Возвращаем статус 201 Created и созданный объект
            return CreatedAtAction(nameof(GetWebsite), new { id = website.Id }, website);
        }

        // DELETE: api/Websites/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWebsite(int id)
        {
            // Ищем сайт для удаления
            var website = await _context.Websites.FindAsync(id);
            if (website == null)
            {
                return NotFound();
            }

            // Удаляем и сохраняем изменения
            _context.Websites.Remove(website);
            await _context.SaveChangesAsync();

            return NoContent(); // Возвращаем 204 No Content
        }
    }
}