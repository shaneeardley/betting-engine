using System;
using BettingEngineServer.Classes;
using BettingEngineServer.Controllers;
using BettingEngineServer.Interfaces;
using BettingEngineServer.Repositories;
using BettingEngineServer.Services;
using Xunit;

namespace BettingEngineServerTests
{
    public class MarketControllerTests
    {
        private MarketController MarketController { get; set; }

        public MarketControllerTests()
        {
            this.MarketController = new MarketController(new MarketService(new MarketRepository()));
        }
        
        [Fact]
        public void CanCreateMarket()
        {
            var newMarket = MarketController.Post(new Market());
            Assert.NotNull(newMarket.Id);
        }
        
        [Fact]
        public void CanDeleteMarket()
        {
            var newMarket = MarketController.Post(new Market());
            MarketController.Delete(newMarket.Id);
            Assert.Empty(MarketController.Get());
        }

        [Fact]
        public void CanGetAllMarkets()
        {
            MarketController.Post(new Market());
            MarketController.Post(new Market());
            MarketController.Post(new Market());

            Assert.Equal(3, MarketController.Get().Count);
        }

        [Fact]
        public void CanGetSpecificMarket()
        {
            var newMarket = MarketController.Post(new Market(){MarketDescription = "New Market Description"});

            Assert.Equal(MarketController.Get(newMarket.Id).MarketDescription, newMarket.MarketDescription);
        }

        [Fact]
        public void CantGetMissingMarket()
        {
            MarketController.Post(new Market());
            Assert.Null(MarketController.Get(Guid.NewGuid().ToString()));

        }

        [Fact]
        public void CanUpdateExistingMarket()
        {
            var createdMarket = MarketController.Post(new Market(){MarketDescription = "New Market Desc"});

            createdMarket.MarketDescription = "Updated Market Description";
            MarketController.Put(createdMarket);

            Assert.Equal("Updated Market Description",MarketController.Get(createdMarket.Id).MarketDescription);
        }

        [Fact]
        public void CantUpdateMissingMarket()
        {
            Assert.Null(MarketController.Put(new Market() {Id = Guid.NewGuid().ToString()}));
        }
    }
    
    public class MarketServiceTests
    {
        private MarketService MarketService { get; set; }

        public MarketServiceTests()
        {
            MarketService = new MarketService(new MarketRepository());
        }
        
        [Fact]
        public void CanCreateMarket()
        {
            var newMarket = MarketService.CreateMarket(new Market());
            Assert.NotNull(newMarket.Id);
        }
        
        [Fact]
        public void CanDeleteMarket()
        {
            var newMarket = MarketService.CreateMarket(new Market());
            MarketService.DeleteMarket(newMarket.Id);
            Assert.Empty(MarketService.GetAll());
        }

        [Fact]
        public void CanGetAllMarkets()
        {
            MarketService.CreateMarket(new Market());
            MarketService.CreateMarket(new Market());
            MarketService.CreateMarket(new Market());

            Assert.Equal(3, MarketService.GetAll().Count);
        }

        [Fact]
        public void CanGetSpecificMarket()
        {
            var newMarket = MarketService.CreateMarket(new Market(){MarketDescription = "New Market Description"});

            Assert.Equal(MarketService.GetById(newMarket.Id).MarketDescription, newMarket.MarketDescription);
        }

        [Fact]
        public void CantGetMissingMarket()
        {
            MarketService.CreateMarket(new Market());
            Assert.Null(MarketService.GetById(Guid.NewGuid().ToString()));

        }

        [Fact]
        public void CanUpdateExistingMarket()
        {
            var createdMarket = MarketService.CreateMarket(new Market(){MarketDescription = "New Market Desc"});

            createdMarket.MarketDescription = "Updated Market Description";
            MarketService.UpdateMarket(createdMarket);

            Assert.Equal("Updated Market Description",MarketService .GetById(createdMarket.Id).MarketDescription);
        }

        [Fact]
        public void CantUpdateMissingMarket()
        {
            Assert.Null(MarketService.UpdateMarket(new Market() {Id = Guid.NewGuid().ToString()}));
        }
    }

    public class MarketRepositoryTests
    {
        private ICrudRepository<Market> MarketsRepo { get; set; }

        public MarketRepositoryTests()
        {
            MarketsRepo = new MarketRepository();
        }
        [Fact]
        public void CanCreateMarket()
        {
            var newMarket = MarketsRepo.Create(new Market());
            Assert.NotNull(newMarket.Id);
        }
        
        [Fact]
        public void CanDeleteMarket()
        {
            var newMarket = MarketsRepo.Create(new Market());
            MarketsRepo.Delete(newMarket.Id);
            Assert.Empty(MarketsRepo.GetAll());
        }

        [Fact]
        public void CanGetAllMarkets()
        {
            MarketsRepo.Create(new Market());
            MarketsRepo.Create(new Market());
            MarketsRepo.Create(new Market());

            Assert.Equal(3, MarketsRepo.GetAll().Count);
        }

        [Fact]
        public void CanGetSpecificMarket()
        {
            var newMarket = MarketsRepo.Create(new Market(){MarketDescription = "New Market Description"});

            Assert.Equal(MarketsRepo.GetById(newMarket.Id).MarketDescription, newMarket.MarketDescription);
        }

        [Fact]
        public void CantGetMissingMarket()
        {
            MarketsRepo.Create(new Market());
            Assert.Null(MarketsRepo.GetById(Guid.NewGuid().ToString()));

        }

        [Fact]
        public void CanUpdateExistingMarket()
        {
            var createdMarket = MarketsRepo.Create(new Market(){MarketDescription = "New Market Desc"});

            createdMarket.MarketDescription = "Updated Market Description";
            MarketsRepo.Update(createdMarket);

            Assert.Equal("Updated Market Description", MarketsRepo.GetById(createdMarket.Id).MarketDescription);
        }

        [Fact]
        public void CantUpdateMissingMarket()
        {
            Assert.Null(MarketsRepo.Update(new Market() {Id = Guid.NewGuid().ToString()}));
        }
    }
}