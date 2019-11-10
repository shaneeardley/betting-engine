using System.Collections.Generic;

namespace BettingEngineServer.Classes
{
    public class EventOutcome
    {
        public string EventId { get; set; }
        public string WinningMarketId { get; set; }
        //Negative amounts mean loss
        public decimal PLAmount { get; set; }
        public List<MarketOutcome> PossibleMarketOutcomes { get; set; }
    }
}