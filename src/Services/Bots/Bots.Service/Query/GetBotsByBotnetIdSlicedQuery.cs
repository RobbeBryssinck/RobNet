using System;
using System.Collections.Generic;
using System.Text;
using Bots.Domain.Entities;
using MediatR;

namespace Bots.Service.Query
{
    public class GetBotsByBotnetIdSlicedQuery : IRequest<List<Bot>>
    {
        public int BotnetId { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageIndex { get; set; } = 0;
    }
}
