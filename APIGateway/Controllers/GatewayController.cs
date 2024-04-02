using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace Gateway
{
    [ApiController]
    [Route("[controller]")]
    public class GatewayController : ControllerBase
    {
        private static readonly List<GameInfo> TheInfo = new List<GameInfo>
        {
            new GameInfo { 
                //Id = 1,
                Title = "Failed to retrieve from Microservice",
                //Content = "~/js/snake.js",
                Author = "Failed to retrieve from Microservice",
                DateAdded = "",
                Description = "Failed to retrieve from Microservice",
                HowTo = "Control with arrow keys.",
                //Thumbnail = "/images/snake.jpg" //640x360 resolution
                LeaderBoardStack = new Stack<KeyValuePair<string, int>>(),

    },
            new GameInfo { 
                //Id = 2,
                Title = "Failed to retrieve from Microservice",
                //Content = "~/js/tetris.js",
                Author = "Failed to retrieve from Microservice",
                DateAdded = "",
                Description = "Failed to retrieve from Microservice",
                //Thumbnail = "/images/tetris.jpg"
                LeaderBoardStack = new Stack<KeyValuePair<string, int>>(),
                
            },
            new GameInfo { 
                //Id = 3,
                Title = "Failed to retrieve from Microservice",
                //Content = "~/js/pong.js",
                Author = "Failed to retrieve from Microservice",
                DateAdded = "",
                Description = "Failed to retrieve from Microservice",
                HowTo = "Failed to retrieve from Microservice",
                //Thumbnail = "/images/pong.jpg"
                LeaderBoardStack = new Stack<KeyValuePair<string, int>>(),
            },

        };

        private readonly JsonSerializerOptions options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        private readonly HttpClient client;
        private readonly ILogger<GatewayController> _logger;

        public async Task<GameInfo[]> GetGamesAsync()
        {
            try
            {
                var responseMessage = await this.client.GetAsync("/Micro");

                if (responseMessage != null)
                {
                    var stream = await responseMessage.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<GameInfo[]>(stream, options);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex.Message);
            }
            return new GameInfo[] { };
        }

        /*
        public async Task GetGamesWithInfo()
        {
            GameInfo[] gameInfos = await GetGamesAsync();

            TheInfo
            foreach (Game game in games)
            {
                GameInfo info = gameInfos.FirstOrDefault(x => x.Title == game.Title);
                if (info != null)
                {
                    game.Author = info.Author;
                    game.HowTo = info.HowTo;
                    game.DateAdded = info.DateAdded;
                    game.Description = $"{info.Description} \n {info.DateAdded}";
                    game.LeaderBoard = info.LeaderBoard;
                }
            }

            return games;
        }
        */

        public GatewayController(ILogger<GatewayController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<GameInfo> Get()
        {
            return TheInfo;
        }
    }
}