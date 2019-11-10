using System.Collections.Generic;
using System.Linq;
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

        public Market UpdateMarketProbability(string marketId, in decimal newProbability)
        {
            var existingMarket = MarketRepository.GetById(marketId);
            if (existingMarket == null) return null;
            existingMarket.MarketProbability = newProbability;
            return UpdateMarket(existingMarket);
        }

        public MarketOutcome GetMarketCurrentOutcome(string id)
        {
            var marketById = GetById(id, true);
            if (marketById == null) return null;
            
            var marketOutcome = new MarketOutcome()
            {
                Market = marketById 
            };
            if (marketById.MarketBets == null || marketById.MarketBets.Count == 0) return marketOutcome;
            marketOutcome.MarketLoseProfitAmount = marketById.GetMarketLoseProfitAmount();
            marketOutcome.MarketWinPayoutAmount = marketById.GetMarketWinPayoutAmount();

            return marketOutcome;
        }
    }
}