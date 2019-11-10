using System.Collections.Generic;
using System.Linq;
using BettingEngineServer.Interfaces;

namespace BettingEngineServer.Classes
{
    public class Market : IEntity
    {
        public Market()
        {
            MarketBets = new List<Bet>();
        }
        public string Id { get; set; }
        public decimal MarketProbability { get; set; }
        public string MarketDescription { get; set; }
        public string EventId { get; set; }
        public List<Bet> MarketBets { get; set; }     

        public decimal MarketOdds
        {
            get
            {
                if (MarketProbability == 0) return 0;
                return 1 / MarketProbability;
            }
        }

        public decimal GetMarketLoseProfitAmount()
        {
            if (MarketBets==null || MarketBets.Count == 0) return 0;
            return MarketBets.Sum(b => b.BetAmount);
        }

        public decimal GetMarketWinPayoutAmount()
        {
            var marketMoneyIn = GetMarketLoseProfitAmount();
            return marketMoneyIn * MarketOdds;
        }
        
        
        
    }
}