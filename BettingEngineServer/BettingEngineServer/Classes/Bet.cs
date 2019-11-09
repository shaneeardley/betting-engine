using System;
using BettingEngineServer.Interfaces;

namespace BettingEngineServer.Classes
{
    public class Bet : IEntity
    {
        public string Id { get; set; }
        public string MarketId { get; set; }
        public decimal BetAmount { get; set; }
    }
}