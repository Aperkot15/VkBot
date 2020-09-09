using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Text.Json;
using VkNet.Abstractions;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.Utils;

namespace Vkbot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BotController : ControllerBase
    {
        private readonly ILogger<BotController> _log;
        private readonly IConfiguration _con;
        private readonly IVkApi _vkApi;

        public BotController(ILogger<BotController> logger, IConfiguration config, IVkApi vkApi)
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
                    {
                        var conf = _con["Config:Confitmation"];
                        _log.LogInformation("Confirm with:" + conf);
                        return Ok(conf);
                    }
            }

            switch (json["object:message_new"].ToString())
            {
                case "привет":
                    {
                        var msg = Message.FromJson(new VkResponse(json["object"].ToString()));
                        _vkApi.Messages.Send(new MessagesSendParams
                        {
                            RandomId = new DateTime().Millisecond,
                            PeerId = msg.PeerId.Value,
                            Message = "Вечер в хату"
                        });
                        break;
                    }
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
