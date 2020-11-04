using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bots.API.Infrastructure;
using Bots.API.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace Bots.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BotsController : ControllerBase
    {
        private readonly BotsContext _context;

        public BotsController(BotsContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET api/Bots/5[?pageSize=5&pageIndex=2]
        [HttpGet("{botnetId}")]
        [ProducesResponseType(typeof(IEnumerable<Bot>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBots(int botnetId, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            var botlist = await _context.Bots.Where(x => x.BotnetId == botnetId).ToListAsync();

            var bots = botlist
                .Skip(pageSize * pageIndex)
                .Take(pageSize);

            return Ok(bots);
        }

        // GET: api/Bots/5
        /*
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
        */

        // PUT: api/Bots/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Bot>> PostBot(Bot bot)
        {
            _context.Bots.Add(bot);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBot", new { id = bot.Id }, bot);
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
    }
}
