using System;
using BettingEngineServer.Classes;
using BettingEngineServer.Controllers;

namespace BettingEngineServerTests
{
    public static class Common
    {
        public static Event CreateAndSaveMockEvent(EventController eventController)
        {
            var newEvent = GetValidEvent();
            return eventController.Post(newEvent);
        }

        public static Market CreateAndSaveMockMarket(string eventId, string marketDescription, decimal marketProbability,
            MarketController marketController)
        {
            var newMarket = new Market()
            {
                EventId = eventId,
                MarketDescription = marketDescription,
                MarketProbability = marketProbability
            };

            return marketController.Post(newMarket);
        }

        public static Bet CreateAndSaveMockBet(string marketId, decimal betAmount, BetController betController)
        {
            var newBet = new Bet()
            {
                MarketId = marketId,
                BetAmount = betAmount
            };
            
            return betController.Post(newBet);
        }
        
        public static Event GetValidEvent()
        {
            return new Event()
            {
                EndDate = DateTime.Now.AddDays(1),
                StartDate = DateTime.Now,
                EventDescription = "New Event Name"
            };
        }
    }
}