using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using AutoMapper;
using Bots.Data.Database;
using Bots.Domain.Entities;
using Bots.Service.Command;
using Bots.Service.Query;
using MediatR;
using Bots.API.Models.v1;

namespace Bots.API.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BotsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public BotsController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        // GET api/v1/Bots/5[?pageSize=5&pageIndex=2]
        [HttpGet("{botnetId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Bot>>> BotsAsync(int botnetId, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            try
            {
                var bots = await _mediator.Send(new GetBotsByBotnetIdSlicedQuery
                {
                    BotnetId = botnetId,
                    PageSize = pageSize,
                    PageIndex = pageIndex
                });

                if (bots == null || bots.Count == 0)
                {
                    return NotFound();
                }

                return bots;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("bot/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Bot), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Bot>> BotByIdAsync(int id)
        {
            if (id <= 0)
                return BadRequest();

            var bot = await _mediator.Send(new GetBotByIdQuery
            {
                Id = id
            });

            if (bot != null)
                return bot;

            return NotFound();
        }

        // PUT: api/Bots/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Bot>> PutBot([FromBody]UpdateBotModel updateBotModel)
        {
            try
            {
                var bot = await _mediator.Send(new GetBotByIdQuery
                {
                    Id = updateBotModel.Id
                });

                if (bot == null)
                {
                    return NotFound();
                }

                return await _mediator.Send(new UpdateBotCommand
                {
                    Bot = _mapper.Map(updateBotModel, bot)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Bots
        [HttpPost]
        public async Task<ActionResult<Bot>> PostBot([FromBody]CreateBotModel createBotModel)
        {
            try
            {
                var bot = _mapper.Map<Bot>(createBotModel);
                bot.Status = "Waiting";
                return await _mediator.Send(new CreateBotCommand
                {
                    Bot = bot
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Bots/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Bot>> DeleteBot(int id)
        {
            try
            {
                var bot = await _mediator.Send(new GetBotByIdQuery
                {
                    Id = id
                });

                if (bot == null)
                {
                    return NotFound();
                }

                return await _mediator.Send(new DeleteBotCommand
                {
                    Bot = bot
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
