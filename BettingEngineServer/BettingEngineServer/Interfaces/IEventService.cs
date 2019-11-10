using System.Collections.Generic;
using BettingEngineServer.Classes;

namespace BettingEngineServer.Interfaces
{
    public interface IEventService
    {
        List<Event> GetAll();
        Event GetById(string eventId, bool populateMarkets,bool populateMarketBets);
        Event UpdateEvent(Event existingEvent);
        Event CreateEvent(Event newEvent);
        void DeleteEvent(string eventId);
        EventOutcome GetEventOutcomeForMarket(string eventId, string marketId);
    }
}