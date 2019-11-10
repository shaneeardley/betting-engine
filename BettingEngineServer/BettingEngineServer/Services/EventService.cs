using System;
using System.Collections.Generic;
using System.Linq;
using BettingEngineServer.Classes;
using BettingEngineServer.Interfaces;

namespace BettingEngineServer.Services
{
    public class EventService : IEventService
    {
        private IEventRepository EventRepository { get; set; }
        private IMarketService MarketService { get; set; }
        
        public EventService(IEventRepository eventRepository, IMarketService marketService)
        {
            EventRepository = eventRepository;
            MarketService = marketService;
        }

        

        public List<Event> GetAll()
        {
            return EventRepository.GetAll();
        }

        public Event GetById(string eventId, bool populateMarkets, bool populateMarketBets)
        {
            var byIdEvent = EventRepository.GetById(eventId);
            if (!populateMarkets || byIdEvent==null) return byIdEvent;
            byIdEvent.EventMarkets = MarketService.GetByEventId(eventId, populateMarketBets);
            return byIdEvent;
        }

        public Event UpdateEvent(Event existingEvent)
        {
            return EventRepository.Update(existingEvent);
        }

        public Event CreateEvent(Event newEvent)
        {
            return EventRepository.Create(newEvent);
        }

        public void DeleteEvent(string eventId)
        {
            EventRepository.Delete(eventId);
            
        }

        public EventOutcome GetEventOutcomeForMarket(string eventId, string marketId)
        {
            var eventById = GetById(eventId, true, true);
            if (eventById == null) return null;
            var eventOutcome = new EventOutcome {EventId = eventId};
            if(eventById.EventMarkets==null|| eventById.EventMarkets.Count==0 || eventById.EventMarkets.FirstOrDefault(m=>m.Id==marketId)==null)
                return eventOutcome;
            eventOutcome.WinningMarketId = marketId;
        
            eventById.EventMarkets.ForEach(market =>
            {
                var marketOutcome = new MarketOutcome
                {
                    Market = market,
                    MarketLoseProfitAmount = market.GetMarketLoseProfitAmount(),
                    MarketWinPayoutAmount = market.GetMarketWinPayoutAmount()
                };
                eventOutcome.PossibleMarketOutcomes.Add(marketOutcome);
                if (market.Id == marketId) //Minus payout amount
                    eventOutcome.PLAmount -= marketOutcome.MarketWinPayoutAmount;
                
                eventOutcome.PLAmount += marketOutcome.MarketLoseProfitAmount;
            });

            return eventOutcome;
        }
    }
}