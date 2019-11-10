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
            var newEvent = CreateAndSaveMockEvent();
            CreateAndSaveMockMarket(newEvent.Id, "Team 1 Wins", 0.8m);
            CreateAndSaveMockMarket(newEvent.Id, "Team 2 Wins", 0.6m);
            CreateAndSaveMockMarket(newEvent.Id, "Draw", 0.1m);

            var persistedEvent = EventController.Get(newEvent.Id);
            Assert.Equal(3, persistedEvent.EventMarkets.Count);
        }

        [Fact]
        private void CanInitialiseAndGetMarketWithBets()
        {
            
        }

        [Fact]
        private void CanInitialiseAndGetEventWithMarketsAndBets()
        {
            var newEvent = CreateAndSaveMockEvent();
            var t1win = CreateAndSaveMockMarket(newEvent.Id, "Team 1 Wins", 0.8m);
            var t2win = CreateAndSaveMockMarket(newEvent.Id, "Team 2 Wins", 0.6m);
            var draw = CreateAndSaveMockMarket(newEvent.Id, "Draw", 0.1m);



        }
        
        [Fact]
        private void CanCalculatePAndLPerMarket()
        {
            // Initialise everything
        }

        private Event CreateAndSaveMockEvent()
        {
            var newEvent = new Event()
            {
                StartDate = new DateTime(2019, 11, 2, 11, 0, 0),
                EndDate = new DateTime(2019, 11, 2, 12, 30, 0),
                EventDescription = "RWC: South Africa VS England"
            };
            return EventController.Post(newEvent);
        }

        private Market CreateAndSaveMockMarket(string eventId, string marketDescription, decimal marketProbability)
        {
            var newMarket = new Market()
            {
                EventId = eventId,
                MarketDescription = marketDescription,
                MarketProbability = marketProbability
            };

            return MarketController.Post(newMarket);
        }
        
    }
}