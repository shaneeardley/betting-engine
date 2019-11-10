using System;
using BettingEngineServer.Classes;
using BettingEngineServer.Controllers;
using BettingEngineServer.Repositories;
using BettingEngineServer.Services;
using Xunit;

namespace BettingEngineServerTests
{
    public class EventTests
    {
        private MarketController MarketController { get; set; }
        private EventController EventController { get; set; }
        private BetController BetController { get; set; }
        
        public EventTests()
        {
            var betRepo = new BetRepository();
            var betService = new BetService(betRepo);
            var marketRepo = new MarketRepository();
            var marketService = new MarketService(marketRepo,betService);
            var eventRepo = new EventRepository();
            var eventService = new EventService(eventRepo,marketService);
            
            MarketController = new MarketController(marketService);
            EventController = new EventController(eventService);
            BetController = new BetController(betService);
        }

        [Fact]
        private void CanInitialiseAndGetEventWithMarkets()
        {
            var newEvent =Common.CreateAndSaveMockEvent(EventController);
            Common.CreateAndSaveMockMarket(newEvent.Id, "Team 1 Wins", 0.8m, MarketController);
            Common.CreateAndSaveMockMarket(newEvent.Id, "Team 2 Wins", 0.6m, MarketController);
            Common.CreateAndSaveMockMarket(newEvent.Id, "Draw", 0.1m, MarketController);

            var persistedEvent = EventController.Get(newEvent.Id);
            Assert.Equal(3, persistedEvent.EventMarkets.Count);
        }

        [Fact]
        private void CanInitialiseAndGetMarketWithBets()
        {
            var nMarket = Common.CreateAndSaveMockMarket(
                Guid.NewGuid().ToString(), "Market 1", 0.1m, MarketController);
            Common.CreateAndSaveMockBet(nMarket.Id, 1,BetController);
            Common.CreateAndSaveMockBet(nMarket.Id, 2,BetController);
            Common.CreateAndSaveMockBet(nMarket.Id, 3,BetController);

            var persistedMarket = MarketController.Get(nMarket.Id);
            Assert.Equal(3, persistedMarket.MarketBets.Count);
        }

        [Fact]
        private void CanInitialiseAndGetEventWithMarketsAndBets()
        {
            var newEvent = Common.CreateAndSaveMockEvent(EventController);
           var nMarket1= Common.CreateAndSaveMockMarket(newEvent.Id, "Team 1 Wins", 0.8m,MarketController);
           var nMarket2=  Common.CreateAndSaveMockMarket(newEvent.Id, "Team 2 Wins", 0.6m,MarketController);
           var nMarket3=Common.CreateAndSaveMockMarket(newEvent.Id, "Draw", 0.1m, MarketController);

           for (var i = 0; i < 3; i++)
           {
               Common.CreateAndSaveMockBet(nMarket1.Id, i,BetController);
               Common.CreateAndSaveMockBet(nMarket2.Id, i,BetController);
               Common.CreateAndSaveMockBet(nMarket3.Id, i,BetController);
           }

           var persistedEvent = EventController.GetWithAllChildren(newEvent.Id);

           var success = persistedEvent != null && persistedEvent.EventMarkets != null &&
                         persistedEvent.EventMarkets.Count == 3
                         && persistedEvent.EventMarkets[0].MarketBets != null &&
                         persistedEvent.EventMarkets[0].MarketBets.Count == 3
                         && persistedEvent.EventMarkets[1].MarketBets != null &&
                         persistedEvent.EventMarkets[1].MarketBets.Count == 3
                         && persistedEvent.EventMarkets[2].MarketBets != null &&
                         persistedEvent.EventMarkets[2].MarketBets.Count == 3;

           Assert.True(success);
        }
        
        [Fact]
        private void CanCalculatePAndLPerMarket()
        {
            // Initialise everything
            var newEvent = Common.CreateAndSaveMockEvent(EventController);
            var nMarket1= Common.CreateAndSaveMockMarket(newEvent.Id, "Team 1 Wins", 0.8m,MarketController);
            var nMarket2=  Common.CreateAndSaveMockMarket(newEvent.Id, "Team 2 Wins", 0.6m,MarketController);
            var nMarket3=Common.CreateAndSaveMockMarket(newEvent.Id, "Draw", 0.1m, MarketController);
            for (var i = 0; i < 3; i++)
            {
                Common.CreateAndSaveMockBet(nMarket1.Id, i,BetController);
                Common.CreateAndSaveMockBet(nMarket2.Id, i+1,BetController);
                Common.CreateAndSaveMockBet(nMarket3.Id, i +2,BetController);
            }

            var persistedEvent = EventController.GetWithAllChildren(newEvent.Id);

            EventOutcome outcome1 = EventController.GetProfitAndLossForMarket(newEvent.Id, nMarket1.Id);
            EventOutcome outcome2 = EventController.GetProfitAndLossForMarket(newEvent.Id, nMarket2.Id);
            EventOutcome outcome3 = EventController.GetProfitAndLossForMarket(newEvent.Id, nMarket3.Id);
            

        }

    
        
    }
}