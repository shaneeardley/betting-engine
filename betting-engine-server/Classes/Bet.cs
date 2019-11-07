using System;

namespace betting.engine.server.Classes
{
    public class Bet
    {
        
        public string Id { get; set; }

        public Market BetMarket { get; set; }
        public String BetMarketId { get; set; }
        public decimal BetAmount { get; set; }
    }
}