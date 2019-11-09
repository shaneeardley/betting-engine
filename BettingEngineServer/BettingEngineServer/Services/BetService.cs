using System.Collections.Generic;
using BettingEngineServer.Classes;
using BettingEngineServer.Interfaces;

namespace BettingEngineServer.Services
{
    public class BetService : IBetService
    {
        
        private ICrudRepository<Bet> BetRepository { get; set; }
        
        public BetService(ICrudRepository<Bet> betRepository)
        {
            this.BetRepository = betRepository;
        }
        public List<Bet> GetAll()
        {
            return BetRepository.GetAll();
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