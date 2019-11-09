using System;
using System.Collections.Generic;
using BettingEngineServer.Interfaces;

namespace BettingEngineServer.Classes
{
    public class Event :IEntity
    {
        public string Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string EventDescription { get; set; }
        public List<Market> EventMarkets { get; set; }
    }
}