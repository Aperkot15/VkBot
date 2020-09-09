using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace Vkbot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BotController : ControllerBase
    {
        public ILogger<BotController> Log { get; }
        private readonly IConfiguration Configur;

        public BotController(ILogger<BotController> logger,IConfiguration config)
        {
            Log = logger;
            Configur = config;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("vk bot!");
        }
        [HttpPost("callback")]
        public IActionResult CallBack(JsonElement data) 
        {
            var json = JObject.Parse(data.GetRawText());
            Log.LogInformation("Json data is:" + Configur["Config:Confitmation"]);

            switch (json["type"].ToString())
            {
                case "confirmation":
                    var conf = Configur["Config:Confitmation"];
                    Log.LogInformation("Confirm with:" + conf);
                    return Ok(conf);
                default:
                    break;
            }

            return Ok();
        }

        [HttpGet("callback")]
        public IActionResult CallBackGet()
        {
            return Ok("Callback test!");
        }
    }
}
