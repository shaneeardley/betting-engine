using System;
using System.Diagnostics.Eventing.Reader;
using BettingEngineServer.Classes;
using BettingEngineServer.Controllers;
using BettingEngineServer.Interfaces;
using BettingEngineServer.Repositories;
using BettingEngineServer.Services;
using Xunit;

namespace BettingEngineServerTests
{
    public class BetControllerTests
    {
        private BetController BetController { get; set; }
        public EventController EventController { get; set; }
        public MarketController MarketController { get; set; }
        public EventRepository EventRepository { get; set; }

        public BetControllerTests()
        {
            
            var betRepo = new BetRepository();
            var marketRepo = new MarketRepository();
            EventRepository = new EventRepository();
            var betService = new BetService(betRepo,EventRepository,marketRepo);
            var marketService = new MarketService(marketRepo,betService);
            var eventService = new EventService(EventRepository,marketService);
            
            MarketController = new MarketController(marketService);
            EventController = new EventController(eventService);
            BetController = new BetController(betService);
        }
        
          
        [Fact]
        public void CanPostValidBet()
        {    
            var mockEvent = Common .CreateAndSaveMockEvent(EventController);
            var mockMarket = Common.CreateAndSaveMockMarket(mockEvent.Id, "Market Desc", 0.5m, MarketController);
            var mockBet = Common.CreateAndSaveMockBet(mockMarket.Id, 100, BetController);

            Assert.NotNull(mockBet.Id);
        }
        
        [Fact]
        public void CantPostBetWithoutAmount()
        {
            var exceptionText = "";
            try
            {
                Common.CreateAndSaveMockBet(Guid.NewGuid().ToString(), 0, BetController);

            }
            catch (Exception e)
            {
                exceptionText = e.Message;
            }

            Assert.Equal("A bet needs an amount", exceptionText);
        }

        [Fact]
        public void CantPostBetWithoutLinkedMarket()
        {
            var exceptionText = "";
            try
            {
                Common.CreateAndSaveMockBet("", 100, BetController);

            }
            catch (Exception e)
            {
                exceptionText = e.Message;
            }

            Assert.Equal("A bet needs to be linked to a market", exceptionText);
        }
        
        [Fact]
        public void CantPostBetWithInvalidLinkedMarket()
        {
            var exceptionText = "";
            var marketId = Guid.NewGuid().ToString();
            try
            {
                Common.CreateAndSaveMockBet(marketId, 100, BetController);

            }
            catch (Exception e)
            {
                exceptionText = e.Message;
            }

            Assert.Equal($"The linked market with id  {marketId} could not be found.", exceptionText);
        }

        
        [Fact]
        public void CantPostBetOnCompletedEvent()
        {
            var mockEvent = EventRepository.Create(new Event()
            {
                EventDescription = "empty", EndDate = DateTime.Now.AddDays(-1), StartDate = DateTime.Now.AddDays(-2),
            });
            var mockMarket = Common.CreateAndSaveMockMarket(mockEvent.Id, "Market Desc", 0.5m, MarketController);
          
            var exceptionText = "";
            try
            {
                Common.CreateAndSaveMockBet(mockMarket.Id, 100, BetController);

            }
            catch (Exception e)
            {
                exceptionText = e.Message;
            }

            Assert.Equal("Bet cannot be placed, the event has ended.", exceptionText);
        }
        
        [Fact]
        public void CanDeleteBet()
        {
            var mockEvent = Common .CreateAndSaveMockEvent(EventController);
            var mockMarket = Common.CreateAndSaveMockMarket(mockEvent.Id, "Market Desc", 0.5m, MarketController);
            var mockBet = Common.CreateAndSaveMockBet(mockMarket.Id, 100, BetController);
            
            BetController.Delete(mockBet.Id);
            Assert.Empty(BetController.Get());
        }

        [Fact]
        public void CanGetAllBets()
        {
            var mockEvent = Common .CreateAndSaveMockEvent(EventController);
            var mockMarket = Common.CreateAndSaveMockMarket(mockEvent.Id, "Market Desc", 0.5m, MarketController);
            var mockBet = Common.CreateAndSaveMockBet(mockMarket.Id, 100, BetController);
            var mockBet2 = Common.CreateAndSaveMockBet(mockMarket.Id, 100, BetController);
            var mockBet3 = Common.CreateAndSaveMockBet(mockMarket.Id, 100, BetController);
            

            Assert.Equal(3, BetController.Get().Count);
        }

        [Fact]
        public void CanGetSpecificBet()
        {
            var mockEvent = Common .CreateAndSaveMockEvent(EventController);
            var mockMarket = Common.CreateAndSaveMockMarket(mockEvent.Id, "Market Desc", 0.5m, MarketController);
            var mockBet = Common.CreateAndSaveMockBet(mockMarket.Id, 100, BetController);

            Assert.Equal(BetController.Get(mockBet.Id).BetAmount, mockBet.BetAmount);
        }

        [Fact]
        public void CantGetMissingBet()
        {
            Assert.Null(BetController.Get(Guid.NewGuid().ToString()));

        }

        [Fact]
        public void CanPutExistingBet()
        {
            var mockEvent = Common .CreateAndSaveMockEvent(EventController);
            var mockMarket = Common.CreateAndSaveMockMarket(mockEvent.Id, "Market Desc", 0.5m, MarketController);
            var mockBet = Common.CreateAndSaveMockBet(mockMarket.Id, 100, BetController);

            mockBet.BetAmount = 234;
            BetController.Put(mockBet);

            Assert.Equal(234,BetController.Get(mockBet.Id).BetAmount);
        }

        [Fact]
        public void CantUpdateMissingBet()
        {
            Assert.Null(BetController.Put(new Bet() {Id = Guid.NewGuid().ToString()}));
        }
        
    }
    
    public class BetServiceTests
    {
        private BetService BetService { get; set; }

        public EventController EventController { get; set; }
        public MarketController MarketController { get; set; }
        
        public BetServiceTests()
        {
            
            var eventRepo = new EventRepository();
            var marketRepo = new MarketRepository();
            var betRepo = new BetRepository();;
            BetService = new BetService(betRepo, eventRepo, marketRepo);
            var marketService = new MarketService(marketRepo,BetService);
            var eventService = new EventService(eventRepo, marketService);
            
            
            MarketController = new MarketController(marketService);
            EventController = new EventController(eventService);
        }
        
        [Fact]
        public void CanCreateBet()
        {
            var mockEvent = Common .CreateAndSaveMockEvent(EventController);
            var mockMarket = Common.CreateAndSaveMockMarket(mockEvent.Id, "Market Desc", 0.5m, MarketController);
            
            var newBet = BetService.CreateBet(new Bet(){BetAmount = 100,MarketId = mockMarket.Id});
            Assert.NotNull(newBet.Id);
        }
        
        [Fact]
        public void CanDeleteBet()
        {
            var mockEvent = Common .CreateAndSaveMockEvent(EventController);
            var mockMarket = Common.CreateAndSaveMockMarket(mockEvent.Id, "Market Desc", 0.5m, MarketController);

            var newBet = BetService.CreateBet(new Bet(){BetAmount = 100,MarketId = mockMarket.Id});
            BetService.DeleteBet(newBet.Id);
            Assert.Empty(BetService.GetAll());
        }

        [Fact]
        public void CanGetAllBets()
        {
            
            var mockEvent = Common .CreateAndSaveMockEvent(EventController);
            var mockMarket = Common.CreateAndSaveMockMarket(mockEvent.Id, "Market Desc", 0.5m, MarketController);

            BetService.CreateBet(new Bet(){BetAmount = 100,MarketId = mockMarket.Id});
            BetService.CreateBet(new Bet(){BetAmount = 100,MarketId = mockMarket.Id});
            BetService.CreateBet(new Bet(){BetAmount = 100,MarketId = mockMarket.Id});

            Assert.Equal(3, BetService.GetAll().Count);
        }

        [Fact]
        public void CanGetSpecificBet()
        {
            var mockEvent = Common .CreateAndSaveMockEvent(EventController);
            var mockMarket = Common.CreateAndSaveMockMarket(mockEvent.Id, "Market Desc", 0.5m, MarketController);

            
            var newBet = BetService.CreateBet(new Bet(){BetAmount = 123,MarketId = mockMarket.Id});

            Assert.Equal(BetService.GetById(newBet.Id).BetAmount, newBet.BetAmount);
        }

        [Fact]
        public void CantGetMissingBet()
        {
            Assert.Null(BetService.GetById(Guid.NewGuid().ToString()));
        }

        [Fact]
        public void CanUpdateExistingBet()
        {
            var mockEvent = Common .CreateAndSaveMockEvent(EventController);
            var mockMarket = Common.CreateAndSaveMockMarket(mockEvent.Id, "Market Desc", 0.5m, MarketController);

            var createdBet = BetService.CreateBet(new Bet(){BetAmount = 123,MarketId = mockMarket.Id});

            createdBet.BetAmount = 234;
            BetService.UpdateBet(createdBet);

            Assert.Equal(234,BetService .GetById(createdBet.Id).BetAmount);
        }

        [Fact]
        public void CantUpdateMissingBet()
        {
            Assert.Null(BetService.UpdateBet(new Bet() {Id = Guid.NewGuid().ToString()}));
        }
    }

    public class BetRepositoryTests
    {
        private ICrudRepository<Bet> BetsRepo { get; set; }

        public BetRepositoryTests()
        {
            BetsRepo = new BetRepository();
        }
        [Fact]
        public void CanCreateBet()
        {
            var newBet = BetsRepo.Create(new Bet());
            Assert.NotNull(newBet.Id);
        }
        
        [Fact]
        public void CanDeleteBet()
        {
            var newBet = BetsRepo.Create(new Bet());
            BetsRepo.Delete(newBet.Id);
            Assert.Empty(BetsRepo.GetAll());
        }

        [Fact]
        public void CanGetAllBets()
        {
            BetsRepo.Create(new Bet());
            BetsRepo.Create(new Bet());
            BetsRepo.Create(new Bet());

            Assert.Equal(3, BetsRepo.GetAll().Count);
        }

        [Fact]
        public void CanGetSpecificBet()
        {
            var newBet = BetsRepo.Create(new Bet(){BetAmount =123});

            Assert.Equal(BetsRepo.GetById(newBet.Id).BetAmount, newBet.BetAmount);
        }

        [Fact]
        public void CantGetMissingBet()
        {
            BetsRepo.Create(new Bet());
            Assert.Null(BetsRepo.GetById(Guid.NewGuid().ToString()));

        }

        [Fact]
        public void CanUpdateExistingBet()
        {
            var createdBet = BetsRepo.Create(new Bet(){BetAmount =1});

            createdBet.BetAmount =2;
            BetsRepo.Update(createdBet);

            Assert.Equal(2, BetsRepo.GetById(createdBet.Id).BetAmount);
        }

        [Fact]
        public void CantUpdateMissingBet()
        {
            Assert.Null(BetsRepo.Update(new Bet() {Id = Guid.NewGuid().ToString()}));
        }
    }
}