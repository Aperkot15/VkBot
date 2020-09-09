﻿using System;
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
    [Route("[controller]")]
    [ApiController]
    public class BotController : ControllerBase
    {
        public ILogger<BotController> Log { get; }
        public IConfiguration Config { get; }

        public BotController(ILogger<BotController> logger,IConfiguration config)
        {
            Log = logger;
            Config = config;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("vk bot!");
        }
        [HttpPost]
        public IActionResult CallBack(JsonElement data) 
        {
            var json = JObject.Parse(data.GetRawText());
            Log.LogInformation("Json data is:" + json);

            switch (json["type"])
            {
                case "confirmation":
                    var conf = Config["Config:Confitmation"];
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
