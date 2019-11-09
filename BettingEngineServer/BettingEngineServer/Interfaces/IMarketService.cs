using System.Collections.Generic;
using BettingEngineServer.Classes;

namespace BettingEngineServer.Interfaces
{
    public interface IMarketService
    {
        List<Market> GetAll();
        Market GetById(string marketId);
        Market UpdateMarket(Market existingMarket);
        Market CreateMarket(Market newMarket);
        void DeleteMarket(string marketId);
    }
}