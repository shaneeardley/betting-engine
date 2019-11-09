using System.Collections.Generic;
using BettingEngineServer.Classes;
using BettingEngineServer.Interfaces;

namespace BettingEngineServer.Services
{
    public class MarketService : IMarketService
    {
        
        private ICrudRepository<Market> MarketRepository { get; set; }
        
        public MarketService(ICrudRepository<Market> marketRepository)
        {
            MarketRepository = marketRepository;
        }
        
        public List<Market> GetAll()
        {
            return MarketRepository.GetAll();
        }

        public Market GetById(string marketId)
        {
            return MarketRepository.GetById(marketId);
        }

        public Market UpdateMarket(Market existingMarket)
        {
            return MarketRepository.Update(existingMarket);
        }

        public Market CreateMarket(Market newMarket)
        {
            return MarketRepository.Create(newMarket);
        }

        public void DeleteMarket(string marketId)
        {
            MarketRepository.Delete(marketId);
        }
    }
}