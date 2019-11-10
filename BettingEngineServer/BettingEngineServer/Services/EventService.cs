using System;
using System.Collections.Generic;
using BettingEngineServer.Classes;
using BettingEngineServer.Interfaces;

namespace BettingEngineServer.Services
{
    public class EventService : IEventService
    {
        private IEventRepository EventRepository { get; set; }
        private IMarketService MarketService { get; set; }
        
        public EventService(IEventRepository eventRepository, IMarketService marketService)
        {
            EventRepository = eventRepository;
            MarketService = marketService;
        }

        

        public List<Event> GetAll()
        {
            return EventRepository.GetAll();
        }

        public Event GetById(string eventId, bool populateMarkets, bool populateMarketBets)
        {
            var byIdEvent = EventRepository.GetById(eventId);
            if (!populateMarkets || byIdEvent==null) return byIdEvent;
            byIdEvent.EventMarkets = MarketService.GetByEventId(eventId, populateMarketBets);
            return byIdEvent;
        }

        public Event UpdateEvent(Event existingEvent)
        {
            return EventRepository.Update(existingEvent);
        }

        public Event CreateEvent(Event newEvent)
        {
            return EventRepository.Create(newEvent);
        }

        public void DeleteEvent(string eventId)
        {
            EventRepository.Delete(eventId);
            
        }
    }
}