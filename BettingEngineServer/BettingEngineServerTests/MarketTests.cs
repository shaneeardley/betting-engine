using System;
using BettingEngineServer.Classes;
using BettingEngineServer.Controllers;
using BettingEngineServer.Repositories;
using BettingEngineServer.Services;
using Xunit;

namespace BettingEngineServerTests
{
    public class MarketTests
    {
        private MarketController MarketController { get; set; }
        private EventController EventController { get; set; }
        private BetController BetController { get; set; }

        public MarketTests()
        {
            var betRepo = new BetRepository();
            var marketRepo = new MarketRepository();
            var eventRepo = new EventRepository();
            var betService = new BetService(betRepo,eventRepo,marketRepo);
            var marketService = new MarketService(marketRepo,betService);
            var eventService = new EventService(eventRepo,marketService);
            
            MarketController = new MarketController(marketService);
            EventController = new EventController(eventService);
            BetController = new BetController(betService);
        }

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

        [Fact]
        private void CanGetAllMarketsByEventId()
        {
            var newEvent = Common.CreateAndSaveMockEvent(EventController);
            Common.CreateAndSaveMockMarket(newEvent.Id, "Team 1 Wins", 0.8m, MarketController);
            Common.CreateAndSaveMockMarket(newEvent.Id, "Team 2 Wins", 0.6m, MarketController);
            Common.CreateAndSaveMockMarket(newEvent.Id, "Draw", 0.1m, MarketController);

            var persistedMarkets = MarketController.GetByEventId(newEvent.Id);
            Assert.Equal(3, persistedMarkets.Count);
        }

        [Fact]
        private void CanModifyMarketProbability()
        {
            var newMarket =
                Common.CreateAndSaveMockMarket(Guid.NewGuid().ToString(), "Market1", 0.9m, MarketController);

            MarketController.UpdateMarketProbability(newMarket.Id, 0.75m);

            var persistedMarket = MarketController.Get(newMarket.Id);
            Assert.Equal(0.75m, persistedMarket.MarketProbability);

        }

        [Fact]
        private void CanCalculateMarketProfitAndPayout()
        {
            var nEvent = Common.CreateAndSaveMockEvent(EventController);
            
            var newMarket = Common.CreateAndSaveMockMarket(nEvent.Id, "Market 1",
                0.5m, MarketController);

            Common.CreateAndSaveMockBet(newMarket.Id, 100, BetController);
            Common.CreateAndSaveMockBet(newMarket.Id, 50, BetController);
            Common.CreateAndSaveMockBet(newMarket.Id, 99.99m, BetController);

            MarketOutcome marketOutcome = MarketController.GetMarketCurrentOutcome(newMarket.Id);

            var success = (marketOutcome!=null) && 
                          (marketOutcome.MarketLoseProfitAmount == 249.99m) &&
                          (marketOutcome.MarketWinPayoutAmount == 499.98m);

            Assert.True(success);
        }

        [Fact]
        private void CantCalculateMarketProfitAndPayoutWithoutBets()
        {
            var newMarket = Common.CreateAndSaveMockMarket(Guid.NewGuid().ToString(), "Market 1",
                0.5m, MarketController);
            var marketOutcome = MarketController.GetMarketCurrentOutcome(newMarket.Id);
            var success = marketOutcome != null && marketOutcome.MarketLoseProfitAmount == 0 &&
                          marketOutcome.MarketWinPayoutAmount == 0;
            Assert.True(success);
        }
        
        [Fact]
        private void CantCalculateMarketProfitAndPayoutWithoutMarket()
        {
            var marketOutcome = MarketController.GetMarketCurrentOutcome(null);
            var success = marketOutcome == null;
            Assert.True(success);
        }
    }
}