using System;
using System.Collections.Generic;
using System.Linq;
using BettingEngineServer.Classes;
using BettingEngineServer.Interfaces;

namespace BettingEngineServer.Repositories
{
    public class MarketRepository : ICrudRepository<Market>
    {
        public Dictionary<string,Market> CachedMarkets { get; set; }

        public MarketRepository()
        {
            CachedMarkets = new Dictionary<string, Market>();
        }
        
        public List<Market> GetAll()
        {
            return CachedMarkets.Values?.ToList();
        }

        public Market GetById(string id)
        {
            return CachedMarkets.TryGetValue(id, out var market) ? market : null;
        }

        public Market Create(Market newItem)
        {
            newItem.Id = Guid.NewGuid().ToString();
            CachedMarkets.Add(newItem.Id, newItem);
            return newItem;
        }

        public Market Update(Market updateItem)
        {
            if (CachedMarkets.TryGetValue(updateItem.Id, out _))
            {
                CachedMarkets[updateItem.Id] = updateItem;
                return updateItem;
            }

            return null;
        }

        public void Delete(string id)
        {
            CachedMarkets.Remove(id);
        }
    }
}