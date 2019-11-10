using System.Collections.Generic;
using BettingEngineServer.Classes;

namespace BettingEngineServer.Interfaces
{
    public interface IMarketService
    {
        List<Market> GetAll();

        List<Market> GetByEventId(string eventId, bool populateBets);
        Market GetById(string marketId, bool populateBets);
        Market UpdateMarket(Market existingMarket);
        Market CreateMarket(Market newMarket);
        void DeleteMarket(string marketId);
    }
}