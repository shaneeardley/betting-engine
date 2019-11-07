using System.Collections.Generic;

namespace betting.engine.server.Classes
{
    public class Market
    {
        
        public string Id { get; set; }
        public string EventId { get; set; }
        public string MarketDescription { get; set; }
        public int MarketProbability { get; set; }
        public List<Bet> MarketBets { get; set; }
    }
}