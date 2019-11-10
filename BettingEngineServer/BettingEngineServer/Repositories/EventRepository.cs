using System;
using System.Collections.Generic;
using System.Linq;
using BettingEngineServer.Classes;
using BettingEngineServer.Interfaces;

namespace BettingEngineServer.Repositories
{
    public class EventRepository : IEventRepository
    {
        public Dictionary<string,Event> CachedEvents { get; set; }

        public EventRepository()
        {
            this.CachedEvents = new Dictionary<string, Event>();
        }
        
        public List<Event> GetAll()
        {
            return CachedEvents.Values?.ToList();
        }

        public Event GetById(string id)
        {
            return CachedEvents.TryGetValue(id, out var exEvent) ? exEvent : null;
        }

        public Event Create(Event newItem)
        {
            newItem.Id = Guid.NewGuid().ToString();
            CachedEvents.Add(newItem.Id, newItem);
            return newItem;
        }

        public Event Update(Event updateItem)
        {
            if (CachedEvents.TryGetValue(updateItem.Id, out _))
            {
                CachedEvents[updateItem.Id] = updateItem;
                return updateItem;
            }

            return null;
        }

        public void Delete(string id)
        {
            CachedEvents.Remove(id);
        }
    }
}