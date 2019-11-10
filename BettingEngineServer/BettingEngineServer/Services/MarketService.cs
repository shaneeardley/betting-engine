using System.Collections.Generic;
using BettingEngineServer.Classes;
using BettingEngineServer.Interfaces;

namespace BettingEngineServer.Services
{
    public class MarketService : IMarketService
    {
        
        private IMarketRepository MarketRepository { get; set; }
        private IBetService BetService { get; set; }

        public MarketService(IMarketRepository marketRepository, IBetService betService)
        {
            MarketRepository = marketRepository;
            BetService = betService;
        }
        
        public List<Market> GetAll()
        {
            return MarketRepository.GetAll();
        }

        public List<Market> GetByEventId(string eventId, bool populateBets)
        {
            List<Market> byEventId = MarketRepository.GetAllByEventId(eventId);
            if (!populateBets) return byEventId;
            byEventId.ForEach(market => { market.MarketBets = BetService.GetAllByMarketId(market.Id); });
            return byEventId;
        }

        public Market GetById(string marketId, bool populateBets)
        {
            var marketById = MarketRepository.GetById(marketId);
            if (!populateBets || marketById==null) return marketById;
            marketById.MarketBets = BetService.GetAllByMarketId(marketId);
            return marketById;
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