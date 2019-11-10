using System.Collections.Generic;

namespace BettingEngineServer.Classes
{
    public class EventOutcome
    {

        public EventOutcome()
        {
            PossibleMarketOutcomes = new List<MarketOutcome>();
        }
        public string EventId { get; set; }
        public string WinningMarketId { get; set; }

        public Market WinningMarket { get; set; }
        //Negative amounts mean loss
        public decimal PLAmount { get; set; }
        public List<MarketOutcome> PossibleMarketOutcomes { get; set; }
    }
}