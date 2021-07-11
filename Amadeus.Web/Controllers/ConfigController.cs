using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Amadeus.Db;
using Amadeus.Db.Models;

namespace Amadeus.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly AmadeusContext _context;

        public ConfigController(AmadeusContext context)
        {
            _context = context;
        }

        // GET: api/Config
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Config>>> GetConfigs()
        {
            return await _context.Configs.ToListAsync();
        }

        // GET: api/Config/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Config>> GetConfig(int id)
        {
            var config = await _context.Configs.FindAsync(id);

            if (config == null)
            {
                return NotFound();
            }

            return config;
        }

        // PUT: api/Config/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutConfig(int id, Config config)
        {
            if (id != config.Id)
            {
                return BadRequest();
            }

            _context.Entry(config).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConfigExists(id))
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

        // POST: api/Config
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Config>> PostConfig(Config config)
        {
            _context.Configs.Add(config);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConfig", new { id = config.Id }, config);
        }

        // DELETE: api/Config/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteConfig(int id)
        {
            var config = await _context.Configs.FindAsync(id);
            if (config == null)
            {
                return NotFound();
            }

            _context.Configs.Remove(config);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConfigExists(int id)
        {
            return _context.Configs.Any(e => e.Id == id);
        }
    }
}
