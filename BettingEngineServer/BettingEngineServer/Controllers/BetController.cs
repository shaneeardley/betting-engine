using System.Collections.Generic;
using BettingEngineServer.Classes;
using BettingEngineServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BettingEngineServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BetController : ControllerBase
    {
        
        public BetService BetService { get; set; }

        private readonly ILogger<BetController> _logger;
        
        public BetController( BetService betService)
        {
            this.BetService = betService;
        }
            
        [HttpGet("{id}")]
        public Bet Get(string id)
        {
            return BetService.GetById(id);
        }
        
        [HttpGet]
        public List<Bet> Get()
        {
            return BetService.GetAll();
        }
        
        [HttpPost]
        public Bet Post(Bet newBet)
        {
            return BetService.CreateBet(newBet);
        }

        [HttpPut]
        public Bet Put(Bet updateBet)
        {
            return BetService.UpdateBet(updateBet);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            BetService.DeleteBet(id);
        }
    }
}