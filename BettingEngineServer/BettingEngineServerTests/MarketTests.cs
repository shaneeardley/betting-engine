using System;
using BettingEngineServer.Classes;
using Xunit;

namespace BettingEngineServerTests
{
    public class MarketTests
    {

        [Fact]
        private void CanCalculateMarketOdds()
        {
            Market newMarket = new Market()
            {
                MarketProbability = 0.5m
            };
            Assert.Equal(2, newMarket.MarketOdds);
        }

        [Fact]
        private void CantCalculateMarketOddsWithNoProbability()
        {
            Market newMarket = new Market();
            Assert.Equal(0, newMarket.MarketOdds);
        }
    }
}