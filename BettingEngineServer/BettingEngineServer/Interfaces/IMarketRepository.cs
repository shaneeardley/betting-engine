using System.Collections.Generic;
using BettingEngineServer.Classes;

namespace BettingEngineServer.Interfaces
{
    public interface IMarketRepository : ICrudRepository<Market>
    {
        List<Market> GetAllByEventId(string eventId);
    }
}