using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bots.API.Infrastructure;
using Bots.API.Models;

namespace Bots.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BotnetsController : ControllerBase
    {
        private readonly BotsContext _context;

        public BotnetsController(BotsContext context)
        {
            _context = context;
        }

        // GET: api/Botnets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Botnet>>> GetBotnets()
        {
            return await _context.Botnets.ToListAsync();
        }

        // GET: api/Botnets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Botnet>> GetBotnet(int id)
        {
            var botnet = await _context.Botnets.FindAsync(id);

            if (botnet == null)
            {
                return NotFound();
            }

            return botnet;
        }

        // PUT: api/Botnets/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBotnet(int id, Botnet botnet)
        {
            if (id != botnet.Id)
            {
                return BadRequest();
            }

            _context.Entry(botnet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BotnetExists(id))
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

        // POST: api/Botnets
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Botnet>> PostBotnet(Botnet botnet)
        {
            _context.Botnets.Add(botnet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBotnet", new { id = botnet.Id }, botnet);
        }

        // DELETE: api/Botnets/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Botnet>> DeleteBotnet(int id)
        {
            var botnet = await _context.Botnets.FindAsync(id);
            if (botnet == null)
            {
                return NotFound();
            }

            _context.Botnets.Remove(botnet);
            await _context.SaveChangesAsync();

            return botnet;
        }

        private bool BotnetExists(int id)
        {
            return _context.Botnets.Any(e => e.Id == id);
        }
    }
}
