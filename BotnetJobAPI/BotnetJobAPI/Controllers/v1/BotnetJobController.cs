using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BotnetJobAPI.Domain.Entities;
using BotnetJobAPI.Models.v1;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using BotnetJobAPI.Service.v1.Command;
using BotnetJobAPI.Service.v1.Query;

namespace BotnetJobAPI.Controllers.v1
{
    [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]
    public class BotnetJobController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public BotnetJobController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<ActionResult<BotnetJob>> BotnetJob([FromBody] CreateBotnetJobModel createBotnetJobModel)
        {
            try
            {
                return await _mediator.Send(new CreateBotnetJobCommand
                {
                    BotnetJob = _mapper.Map<BotnetJob>(createBotnetJobModel)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<ActionResult<BotnetJob>> BotnetJob([FromBody] UpdateBotnetJobModel updateBotnetJobModel)
        {
            try
            {
                var botnetJob = await _mediator.Send(new GetBotnetJobByIdQuery
                {
                    Id = updateBotnetJobModel.Id
                });

                if (botnetJob == null)
                {
                    return BadRequest($"No botnet job found with id {updateBotnetJobModel.Id}");
                }

                return await _mediator.Send(new UpdateBotnetJobCommand
                {
                    BotnetJob = _mapper.Map(updateBotnetJobModel, botnetJob)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
