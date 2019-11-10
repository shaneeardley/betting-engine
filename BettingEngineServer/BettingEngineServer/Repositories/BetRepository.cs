using System;
using System.Collections.Generic;
using System.Linq;
using BettingEngineServer.Classes;
using BettingEngineServer.Interfaces;

namespace BettingEngineServer.Repositories
{ 
    public class BetRepository: IBetRepository  
    {
        public Dictionary<string,Bet>  CachedBets { get; set; }
        public BetRepository()
        {
            CachedBets = new  Dictionary<string, Bet>();
        }
        
        public List<Bet> GetAll()
        {
            return CachedBets.Values?.ToList();
        }

        public Bet GetById(string id)
        {
            return CachedBets.TryGetValue(id, out var bet) ? bet : null;
        }

        public Bet Create(Bet newItem)
        {
            newItem.Id = Guid.NewGuid().ToString();
            CachedBets.Add(newItem.Id, newItem);
            return newItem;
        }

        public Bet Update(Bet updateItem)
        { 
            if (CachedBets.TryGetValue(updateItem.Id, out _))
            {
                CachedBets[updateItem.Id] = updateItem;
                return updateItem;
            }

            return null;
        }

        public void Delete(string id)
        {
            CachedBets.Remove(id);
        }

        public List<Bet> GetAllByMarketId(string marketId)
        {
            return CachedBets?.Values?.ToList().Where(b => b.MarketId == marketId)?.ToList();
        }
    }
}