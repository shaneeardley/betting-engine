using System;
using BettingEngineServer.Classes;
using BettingEngineServer.Controllers;

namespace BettingEngineServerTests
{
    public static class Common
    {
        public static Event CreateAndSaveMockEvent(EventController eventController)
        {
            var newEvent = new Event()
            {
                StartDate = new DateTime(2019, 11, 2, 11, 0, 0),
                EndDate = new DateTime(2019, 11, 2, 12, 30, 0),
                EventDescription = "RWC: South Africa VS England"
            };
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
    }
}