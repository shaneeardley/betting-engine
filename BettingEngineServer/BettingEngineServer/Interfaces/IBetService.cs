using System.Collections.Generic;
using BettingEngineServer.Classes;

namespace BettingEngineServer.Interfaces
{
    public interface IBetService
    {
        List<Bet> GetAll();
        List<Bet> GetAllByMarketId(string marketId);
        Bet GetById(string betId);
        Bet UpdateBet(Bet existingBet);
        Bet CreateBet(Bet newBet);
        void DeleteBet(string betId);
    }
}