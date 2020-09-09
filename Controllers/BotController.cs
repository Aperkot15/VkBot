
using VkNet.Abstractions;
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
        private readonly ILogger<BotController> _log;
        private readonly IConfiguration _con;
        private readonly IVkApi _vkApi;

        public BotController(ILogger<BotController> logger,IConfiguration config,IVkApi vkApi)
        {
            _log = logger;
            _vkApi = vkApi;
            _con = config;
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
            _log.LogInformation("Json data is:" + _con["Config:Confitmation"]);

            switch (json["type"].ToString())
            {
                case "confirmation":
                    var conf = _con["Config:Confitmation"];
                    _log.LogInformation("Confirm with:" + conf);
                    return Ok(conf);
                default:
                    break;
            }

            switch (json["object:message_new"].ToString()) 
            {
                case "привет":
                    var message = "Вечер в хату мой повелитель";
                    return Ok(message);
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
