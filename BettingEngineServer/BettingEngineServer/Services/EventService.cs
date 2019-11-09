using System;
using System.Collections.Generic;
using BettingEngineServer.Classes;
using BettingEngineServer.Interfaces;

namespace BettingEngineServer.Services
{
    public class EventService : IEventService
    {
        private ICrudRepository<Event> EventRepository { get; set; }
        
        public EventService(ICrudRepository<Event> eventRepository)
        {
            this.EventRepository = eventRepository;
        }
        
        public List<Event> GetAll()
        {
            return EventRepository.GetAll();
        }

        public Event GetById(string eventId)
        {
            return EventRepository.GetById(eventId);
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