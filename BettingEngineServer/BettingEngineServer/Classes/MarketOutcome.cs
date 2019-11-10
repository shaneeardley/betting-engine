namespace BettingEngineServer.Classes
{
    public class MarketOutcome
    {
        public Market Market { get; set; }
        public decimal MarketWinPayoutAmount { get; set; }
        public decimal MarketLoseProfitAmount { get; set; }
    }
}