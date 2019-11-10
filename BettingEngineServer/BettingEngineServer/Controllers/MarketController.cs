using System.Collections.Generic;
using BettingEngineServer.Classes;
using BettingEngineServer.Interfaces;
using BettingEngineServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BettingEngineServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MarketController :ControllerBase
    {
        public IMarketService MarketService { get; set; }

        private readonly ILogger<MarketController> _logger;
        
        public MarketController( IMarketService marketService)
        {
            MarketService = marketService;
        }
            
        [HttpGet("{id}")]
        public Market Get(string id)
        {
            return MarketService.GetById(id, true);
        }
        
        [HttpGet]
        public List<Market> Get()
        {
            return MarketService.GetAll();
        }

        [HttpGet("byEventId/{eventId}")]
        public List<Market> GetByEventId(string eventId)
        {
            return MarketService.GetByEventId(eventId, false);
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

        [HttpPost("updateProbability/{id}/{newProbability}")]
        public Market UpdateMarketProbability(string id, decimal newProbability)
        {
            return MarketService.UpdateMarketProbability(id, newProbability);
        }

        [HttpGet("getMarketOutcome/{id}")]
        public MarketOutcome GetMarketCurrentOutcome(string id)
        {
            return MarketService.GetMarketCurrentOutcome(id);
        }
    }
}