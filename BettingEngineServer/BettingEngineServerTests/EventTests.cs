﻿using System;
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
            var nEvent = Common.CreateAndSaveMockEvent(EventController);
            
            var nMarket = Common.CreateAndSaveMockMarket(
                nEvent.Id, "Market 1", 0.1m, MarketController);
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

           for (var i = 1; i < 4; i++)
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
            for (var i = 1; i < 4; i++)
            {
                Common.CreateAndSaveMockBet(nMarket1.Id, i,BetController);
                Common.CreateAndSaveMockBet(nMarket2.Id, i,BetController);
                Common.CreateAndSaveMockBet(nMarket3.Id, i,BetController);
            }

            var persistedEvent = EventController.GetWithAllChildren(newEvent.Id);

            //Bets total 6 per market
            var outcome1 = EventController.GetEventOutcomeForMarket(newEvent.Id, nMarket1.Id); // Payout 7.5
            var outcome2 = EventController.GetEventOutcomeForMarket(newEvent.Id, nMarket2.Id); // Payout 10
            var outcome3 = EventController.GetEventOutcomeForMarket(newEvent.Id, nMarket3.Id); // Payout 60

            var success = outcome1.PLAmount == 10.5m && outcome2.PLAmount == 8m && outcome3.PLAmount == -42;

            Assert.True(success);
        }

    
        
    }
}