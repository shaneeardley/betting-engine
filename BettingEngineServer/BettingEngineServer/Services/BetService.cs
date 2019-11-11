using System;
using System.Collections.Generic;
using BettingEngineServer.Classes;
using BettingEngineServer.Interfaces;

namespace BettingEngineServer.Services
{
    public class BetService : IBetService
    {
        
        private IBetRepository BetRepository { get; set; }
        private IMarketRepository MarketRepository { get; set; }
        public IEventRepository EventRepository { get; set; }

        public BetService(IBetRepository betRepository, IEventRepository eventRepository, IMarketRepository marketRepository)
        {
            BetRepository = betRepository;
            EventRepository = eventRepository;
            MarketRepository = marketRepository;
        }
        public List<Bet> GetAll()
        {
            return BetRepository.GetAll();
        }

        public List<Bet> GetAllByMarketId(string marketId)
        {
            return BetRepository.GetAllByMarketId(marketId);
        }

        public Bet GetById(string betId)
        {
            return BetRepository.GetById(betId);
        }

        public Bet UpdateBet(Bet existingBet)
        {
            return BetRepository.Update(existingBet);
        }

        public Bet CreateBet(Bet newBet)
        {
            //validate new market
            validateNewBet(newBet);
            return BetRepository.Create(newBet);
        }
        
        private void validateNewBet(Bet newBet)
        {
            if (newBet == null) throw new ArgumentNullException(nameof(newBet));
            if (newBet.BetAmount == 0 )
                throw new Exception("A bet needs an amount");
            if(string.IsNullOrEmpty(newBet.MarketId))
                throw new Exception("A bet needs to be linked to a market");
            var linkedMarket = MarketRepository.GetById(newBet.MarketId);
            if (linkedMarket == null)
                throw new Exception($"The linked market with id  {newBet.MarketId} could not be found.");
            if(string.IsNullOrEmpty(linkedMarket.EventId))
                throw new Exception("No parent event could be found for the linked market.");

            var linkedEvent = EventRepository.GetById(linkedMarket.EventId);
            if (linkedEvent == null)
                throw new Exception($"The linked event with id  {linkedMarket.EventId} could not be found.");
            if (linkedEvent.EndDate < DateTime.Now)
                throw new Exception("Bet cannot be placed, the event has ended.");
        }

        public void DeleteBet(string betId)
        {
            BetRepository.Delete(betId);
        }
    }
}