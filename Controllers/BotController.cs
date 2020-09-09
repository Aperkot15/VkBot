using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Vkbot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BotController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("vk bot!");
        }
        [HttpPost("callback")]
        public IActionResult CallBack(JsonElement data) 
        {
            
            return Ok();
        }
        [HttpGet("callback")]
        public IActionResult CallBackGet()
        {
            return Ok("Callback test!");
        }
    }
}
