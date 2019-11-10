using System;
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

        public BetControllerTests()
        {
            BetController = new BetController(new BetService(new BetRepository()));
        }
        
          
        [Fact]
        public void CanPostValidBet()
        {   
            var newBet = BetController.Post(new Bet());
            Assert.NotNull(newBet.Id);
        }
        
        [Fact]
        public void CanDeleteBet()
        {
            var newBet = BetController.Post(new Bet());
            BetController.Delete(newBet.Id);
            Assert.Empty(BetController.Get());
        }

        [Fact]
        public void CanGetAllBets()
        {
            BetController.Post(new Bet());
            BetController.Post(new Bet());
            BetController.Post(new Bet());

            Assert.Equal(3, BetController.Get().Count);
        }

        [Fact]
        public void CanGetSpecificBet()
        {
            var newBet = BetController.Post(new Bet(){BetAmount = 123});

            Assert.Equal(BetController.Get(newBet.Id).BetAmount, newBet.BetAmount);
        }

        [Fact]
        public void CantGetMissingBet()
        {
            BetController.Post(new Bet());
            Assert.Null(BetController.Get(Guid.NewGuid().ToString()));

        }

        [Fact]
        public void CanPutExistingBet()
        {
            var createdBet = BetController.Post(new Bet(){BetAmount = 123});

            createdBet.BetAmount = 234;
            BetController.Put(createdBet);

            Assert.Equal(234,BetController.Get(createdBet.Id).BetAmount);
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

        public BetServiceTests()
        {
            BetService = new BetService(new BetRepository());
        }
        
        [Fact]
        public void CanCreateBet()
        {
            var newBet = BetService.CreateBet(new Bet());
            Assert.NotNull(newBet.Id);
        }
        
        [Fact]
        public void CanDeleteBet()
        {
            var newBet = BetService.CreateBet(new Bet());
            BetService.DeleteBet(newBet.Id);
            Assert.Empty(BetService.GetAll());
        }

        [Fact]
        public void CanGetAllBets()
        {
            BetService.CreateBet(new Bet());
            BetService.CreateBet(new Bet());
            BetService.CreateBet(new Bet());

            Assert.Equal(3, BetService.GetAll().Count);
        }

        [Fact]
        public void CanGetSpecificBet()
        {
            var newBet = BetService.CreateBet(new Bet(){BetAmount = 123});

            Assert.Equal(BetService.GetById(newBet.Id).BetAmount, newBet.BetAmount);
        }

        [Fact]
        public void CantGetMissingBet()
        {
            BetService.CreateBet(new Bet());
            Assert.Null(BetService.GetById(Guid.NewGuid().ToString()));

        }

        [Fact]
        public void CanUpdateExistingBet()
        {
            var createdBet = BetService.CreateBet(new Bet(){BetAmount = 123});

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