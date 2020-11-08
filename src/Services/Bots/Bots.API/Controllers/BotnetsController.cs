using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bots.Domain.Entities;
using Bots.Service.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bots.API.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BotnetsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BotnetsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/Botnets/5
        [HttpGet("{botnetId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Botnet>> BotnetAsync(int botnetId)
        {
            try
            {
                return await _mediator.Send(new GetBotnetByIdQuery
                {
                    Id = botnetId
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
