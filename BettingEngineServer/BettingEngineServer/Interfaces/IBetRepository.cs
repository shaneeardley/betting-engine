using System.Collections.Generic;
using BettingEngineServer.Classes;

namespace BettingEngineServer.Interfaces
{
    public interface IBetRepository : ICrudRepository<Bet>
    {
        List<Bet> GetAllByMarketId(string marketId);
    }
}