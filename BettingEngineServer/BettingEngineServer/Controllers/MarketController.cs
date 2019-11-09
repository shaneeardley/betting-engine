using System.Collections.Generic;
using BettingEngineServer.Classes;
using BettingEngineServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BettingEngineServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MarketController :ControllerBase
    {
        public MarketService MarketService { get; set; }

        private readonly ILogger<MarketController> _logger;
        
        public MarketController( MarketService marketService)
        {
            this.MarketService = marketService;
        }
            
        [HttpGet("{id}")]
        public Market Get(string id)
        {
            return MarketService.GetById(id);
        }
        
        [HttpGet]
        public List<Market> Get()
        {
            return MarketService.GetAll();
        }
        
        [HttpPost]
        public Market Post(Market newMarket)
        {
            return MarketService.CreateMarket(newMarket);
        }

        [HttpPut]
        public Market Put(Market updateMarket)
        {
            return MarketService.UpdateMarket(updateMarket);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            MarketService.DeleteMarket(id);
        }
    }
}