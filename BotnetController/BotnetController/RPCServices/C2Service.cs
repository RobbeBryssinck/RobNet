using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using BotnetController.Logic;
using BotnetController.Data;
using BotnetController.Services;

namespace BotnetController
{
    public class C2Service : C2.C2Base
    {
        private readonly ILogger<C2Service> _logger;
        private readonly C2Context _context;
        public C2Service(ILogger<C2Service> logger, C2Context context)
        {
            _logger = logger;
            _context = context;
        }

        public override Task<StartJobResponse> StartJob(StartJobRequest request, ServerCallContext context)
        {


            return Task.FromResult(new StartJobResponse
            {
                Response = StartJobResponse.Types.Response.Fail,
            });
        }

        /*
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
        */
    }
}
