using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BotnetJobs.API.Infrastructure;
using BotnetJobs.API.Models;

namespace BotnetJobs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BotnetJobsController : ControllerBase
    {
        private readonly BotnetJobsContext _context;

        public BotnetJobsController(BotnetJobsContext context)
        {
            _context = context;
        }

        // GET: api/BotnetJobs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BotnetJob>>> GetBotnetJobs()
        {
            return await _context.BotnetJobs.ToListAsync();
        }

        // GET: api/BotnetJobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BotnetJob>> GetBotnetJob(int id)
        {
            var botnetJob = await _context.BotnetJobs.FindAsync(id);

            if (botnetJob == null)
            {
                return NotFound();
            }

            return botnetJob;
        }

        // PUT: api/BotnetJobs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBotnetJob(int id, BotnetJob botnetJob)
        {
            if (id != botnetJob.Id)
            {
                return BadRequest();
            }

            _context.Entry(botnetJob).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BotnetJobExists(id))
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

        // POST: api/BotnetJobs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<BotnetJob>> PostBotnetJob(BotnetJob botnetJob)
        {
            _context.BotnetJobs.Add(botnetJob);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBotnetJob", new { id = botnetJob.Id }, botnetJob);
        }

        // DELETE: api/BotnetJobs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BotnetJob>> DeleteBotnetJob(int id)
        {
            var botnetJob = await _context.BotnetJobs.FindAsync(id);
            if (botnetJob == null)
            {
                return NotFound();
            }

            _context.BotnetJobs.Remove(botnetJob);
            await _context.SaveChangesAsync();

            return botnetJob;
        }

        private bool BotnetJobExists(int id)
        {
            return _context.BotnetJobs.Any(e => e.Id == id);
        }
    }
}
