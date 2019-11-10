using System.Collections.Generic;
using BettingEngineServer.Classes;
using BettingEngineServer.Interfaces;

namespace BettingEngineServer.Services
{
    public class BetService : IBetService
    {
        
        private IBetRepository BetRepository { get; set; }
        
        public BetService(IBetRepository betRepository)
        {
            BetRepository = betRepository;
        }
        public List<Bet> GetAll()
        {
            return BetRepository.GetAll();
        }

        public List<Bet> GetAllByMarketId(string marketId)
        {
            return BetRepository.GetAllByMarketId(marketId);
        }

        public Bet GetById(string betId)
        {
            return BetRepository.GetById(betId);
        }

        public Bet UpdateBet(Bet existingBet)
        {
            return BetRepository.Update(existingBet);
        }

        public Bet CreateBet(Bet newBet)
        {
            return BetRepository.Create(newBet);
        }

        public void DeleteBet(string betId)
        {
            BetRepository.Delete(betId);
        }
    }
}