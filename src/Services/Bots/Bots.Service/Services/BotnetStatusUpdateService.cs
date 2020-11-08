using System;
using System.Diagnostics;
using Bots.Service.Command;
using Bots.Service.Models;
using Bots.Service.Query;
using MediatR;

namespace Bots.Service.Services
{
    public class BotnetStatusUpdateService : IBotnetStatusUpdateService
    {
        private readonly IMediator _mediator;

        public BotnetStatusUpdateService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async void UpdateBotnetStatus(UpdateBotnetStatusModel updateBotnetStatusModel)
        {
            try
            {
                var botnet = await _mediator.Send(new GetBotnetByIdQuery
                {
                    Id = updateBotnetStatusModel.BotnetId
                });

                if (botnet != null)
                {
                    botnet.Status = updateBotnetStatusModel.Status;
                }

                // TODO: Is this efficient?
                await _mediator.Send(new UpdateBotnetCommand
                {
                    Botnet = botnet
                });
            }
            catch (Exception ex)
            {
                // log error here
                Debug.WriteLine(ex);
            }
        }
    }
}
