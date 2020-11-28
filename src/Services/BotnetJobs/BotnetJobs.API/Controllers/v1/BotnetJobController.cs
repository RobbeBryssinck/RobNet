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
    [Route("api/v1/[controller]")]
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
        /// Action to get a botnet job by BotnetId.
        /// </summary>
        /// <param name="botnetId">Id of the botnet</param>
        /// <returns>The botnet job mapped to BotnetId</returns>
        [HttpGet("{botnetId}")]
        public async Task<ActionResult<BotnetJob>> GetBotnetJobByBotnetIdAsync(int botnetId)
        {
            try
            {
                var botnetJob = await _mediator.Send(new GetBotnetJobByBotnetIdQuery
                {
                    BotnetId = botnetId
                });

                if (botnetJob == null)
                {
                    return NotFound();
                }

                return botnetJob;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
                var botnetJob = _mapper.Map<BotnetJob>(createBotnetJobModel);
                botnetJob.JobAction = "Start";
                botnetJob.Status = "Working";
                return await _mediator.Send(new CreateBotnetJobCommand
                {
                    BotnetJob = botnetJob
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
                    return NotFound();
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

        // TODO: Maybe delete botnetjob by id?
        [HttpDelete("{id}")]
        public async Task<ActionResult<BotnetJob>> DeleteBotnetJob(int id)
        {
            try
            {
                var botnetJob = await _mediator.Send(new GetBotnetJobByIdQuery
                {
                    Id = id
                });

                if (botnetJob == null)
                {
                    return NotFound();
                }

                botnetJob.JobAction = "Stop";
                botnetJob.Status = "Waiting";

                return await _mediator.Send(new DeleteBotnetJobCommand
                {
                    BotnetJob = botnetJob
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
