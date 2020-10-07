using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BotnetAPI.Models;

namespace BotnetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BotsController : ControllerBase
    {
        private readonly BotnetContext _context;

        public BotsController(BotnetContext context)
        {
            _context = context;
        }

        // GET: api/Bots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bot>>> GetBots()
        {
            return await _context.Bots.ToListAsync();
        }

        // GET: api/Bots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bot>> GetBot(int id)
        {
            var bot = await _context.Bots.FindAsync(id);

            if (bot == null)
            {
                return NotFound();
            }

            return bot;
        }

        // PUT: api/Bots/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBot(int id, Bot bot)
        {
            if (id != bot.Id)
            {
                return BadRequest();
            }

            _context.Entry(bot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BotExists(id))
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

        // POST: api/Bots
        [HttpPost]
        public async Task<ActionResult<BotDTO>> PostBot(BotDTO botDTO)
        {
            var bot = new Bot
            {
                UserId = "1",
                IP = botDTO.IP,
                SSHName = botDTO.SSHName,
                Platform = botDTO.Platform,
                Status = null
            };

            _context.Bots.Add(bot);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBot), new { id = bot.Id }, bot);
        }

        // DELETE: api/Bots/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Bot>> DeleteBot(int id)
        {
            var bot = await _context.Bots.FindAsync(id);
            if (bot == null)
            {
                return NotFound();
            }

            _context.Bots.Remove(bot);
            await _context.SaveChangesAsync();

            return bot;
        }

        private bool BotExists(int id)
        {
            return _context.Bots.Any(e => e.Id == id);
        }

        private static BotDTO BotToDTO(Bot bot) =>
            new BotDTO
            {
                Id = bot.Id,
                UserId = bot.UserId,
                IP = bot.IP,
                SSHName = bot.SSHName,
                Platform = bot.Platform,
                Status = bot.Status
            };
    }
}
