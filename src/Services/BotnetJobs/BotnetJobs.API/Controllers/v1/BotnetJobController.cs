using System;
using System.Threading.Tasks;
using AutoMapper;
using BotnetJobs.API.Models.v1;
using BotnetJobs.Domain.Entities;
using BotnetJobs.Service.v1.Command;
using BotnetJobs.Service.v1.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BotnetJobs.API.Controllers.v1
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

        /// <summary>  
        /// Action to create a new botnet job in the database.  
        /// </summary>  
        /// <param name="createBotnetJobModel">Model to create a new botnet job</param>  
        /// <returns>Returns the created botnet job</returns> 
        [HttpPost]
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

        [HttpPut]
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
