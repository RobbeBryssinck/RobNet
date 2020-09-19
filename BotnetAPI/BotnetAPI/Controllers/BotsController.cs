using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotnetAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BotnetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BotsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<Bot> GetBots()
        {

        }
    }
}
