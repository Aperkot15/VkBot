﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace Vkbot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BotController : ControllerBase
    {
        public ILogger<BotController> Log { get; }

        public BotController(ILogger<BotController> logger)
        {
            Log = logger;
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
            Log.LogInformation("Json data is:" + json);
            
            return Ok();
        }
        [HttpGet("callback")]
        public IActionResult CallBackGet()
        {
            return Ok("Callback test!");
        }
    }
}
