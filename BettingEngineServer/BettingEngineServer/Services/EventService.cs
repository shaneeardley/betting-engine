using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            ValidateNewEvent(newEvent);
            
            return EventRepository.Create(newEvent);
        }

        private void ValidateNewEvent(Event newEvent)
        {
            if (newEvent == null) throw new ArgumentNullException(nameof(newEvent));
            if (newEvent.StartDate == null || newEvent.StartDate == new DateTime())
                throw new Exception("An event needs a starting date.");
            if (newEvent.EndDate == null || newEvent.EndDate == new DateTime())
                throw new Exception("An event needs an end date.");
            if(newEvent.EndDate < DateTime.Now)
                throw new Exception("An event cannot be created once the event end date has passed.");
            if (newEvent.StartDate > newEvent.EndDate)
                throw new Exception("An event cannot end before it starts.");
            
            if(string.IsNullOrEmpty(newEvent.EventDescription))
                throw new Exception("An event requires a description in order to be created.");

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
            eventOutcome.WinningMarket = eventById.EventMarkets.FirstOrDefault(m => m.Id == marketId);
        
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