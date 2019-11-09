using System.Collections.Generic;
using BettingEngineServer.Interfaces;

namespace BettingEngineServer.Classes
{
    public class Market : IEntity
    {
        public string Id { get; set; }
        public int MarketProbability { get; set; }
        public string MarketDescription { get; set; }
        public string EventId { get; set; }
        public List<Bet> MarketBets { get; set; }        
    }
}