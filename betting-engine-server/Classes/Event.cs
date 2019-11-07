using System;
using System.Collections.Generic;

namespace betting.engine.server.Classes
{
    public class Event
    {
        public string Id { get; set; }
        public DateTime EventStart { get; set; }
        public DateTime EventEnd { get; set; }
        public string EventName { get; set; }

        public List<Market> EventMarkets { get; set; }
    }
}