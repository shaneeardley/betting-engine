using System;
using System.ComponentModel.DataAnnotations;
using BettingEngineServer.Classes;
using BettingEngineServer.Controllers;
using BettingEngineServer.Interfaces;
using BettingEngineServer.Repositories;
using BettingEngineServer.Services;
using Xunit;

namespace BettingEngineServerTests
{
    public class EventControllerTests
    {
        private EventController EventController { get; set; }

        public EventControllerTests()
        {
            this.EventController = new EventController(new EventService(new EventRepository(),new MarketService(new MarketRepository(), new  BetService(new BetRepository()))));
        }

        
        
        [Fact]
        public void CanCreateValidEvent()
        {
            var newEvent = EventController.Post(Common.GetValidEvent());
            Assert.NotNull(newEvent.Id);
        }

        [Fact]
        public void CantCreateEventWithNoStartDate()
        {
            var newEvent = new Event()
            {
                EndDate = DateTime.Now.AddDays(1),
                EventDescription = "New Event Name"
            };
            Assert.Throws<ValidationException>(() => EventController.Post(newEvent));
        }

        [Fact]
        public void CantCreateEventWithNoEndDate()
        {
            var newEvent = new Event()
            {
                StartDate = DateTime.Now.AddDays(1),
                EventDescription = "New Event Name"
            };
            Assert.Throws<ValidationException>(() => EventController.Post(newEvent));
        }
        
        [Fact]
        public void CantCreateEventWithInvalidEndDate()
        {
            var newEvent = new Event()
            {
                EndDate = DateTime.Now.AddDays(-1),
                StartDate = DateTime.Now,
                EventDescription = "New Event Name"
            };
            Assert.Throws<ValidationException>(() => EventController.Post(newEvent));
        }
        
        [Fact]
        public void CantCreateEventWithNoDescription()
        {
            var newEvent = new Event()
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1)
            };
            Assert.Throws<ValidationException>(() => EventController.Post(newEvent));
        }
        
        
        [Fact]
        public void CanDeleteEvent()
        {
            var newEvent = EventController.Post(Common.GetValidEvent());
            EventController.Delete(newEvent.Id);
            Assert.Empty(EventController.Get());
        }

        [Fact]
        public void CanGetAllEvents()
        {
            EventController.Post(Common.GetValidEvent());
            EventController.Post(Common.GetValidEvent());
            EventController.Post(Common.GetValidEvent());

            Assert.Equal(3, EventController.Get().Count);
        }

        [Fact]
        public void CanGetSpecificEvent()
        {
            var newEvent = EventController.Post(Common.GetValidEvent());

            Assert.Equal(EventController.Get(newEvent.Id).EventDescription, newEvent.EventDescription);
        }

        [Fact]
        public void CanGetSpecificEventAndChildren()
        {
            var newEvent = EventController.Post(Common.GetValidEvent());
                
            Assert.Equal(EventController.GetWithAllChildren(newEvent.Id).EventDescription, newEvent.EventDescription);
        }

        [Fact]
        public void CantGetMissingEvent()
        {
            EventController.Post(Common.GetValidEvent());
            Assert.Null(EventController.Get(Guid.NewGuid().ToString()));

        }

        [Fact]
        public void CanUpdateExistingEvent()
        {
            var createdEvent = EventController.Post(Common.GetValidEvent());

            createdEvent.EventDescription = "Updated Event Description";
            EventController.Put(createdEvent);

            Assert.Equal("Updated Event Description",EventController.Get(createdEvent.Id).EventDescription);
        }

        [Fact]
        public void CantUpdateMissingEvent()
        {
            Assert.Null(EventController.Put(new Event() {Id = Guid.NewGuid().ToString()}));
        }
        
    }
    
    public class EventServiceTests
    {
        private EventService EventService { get; set; }

        public EventServiceTests()
        {
            EventService = new EventService(new EventRepository(), new MarketService(new MarketRepository(), new BetService(new BetRepository())));
        }
        
        [Fact]
        public void CanCreateEvent()
        {
            var newEvent = EventService.CreateEvent(Common.GetValidEvent());
            Assert.NotNull(newEvent.Id);
        }
        
        [Fact]
        public void CanDeleteEvent()
        {
            var newEvent = EventService.CreateEvent(Common.GetValidEvent());
            EventService.DeleteEvent(newEvent.Id);
            Assert.Empty(EventService.GetAll());
        }

        [Fact]
        public void CanGetAllEvents()
        {
            EventService.CreateEvent(Common.GetValidEvent());
            EventService.CreateEvent(Common.GetValidEvent());
            EventService.CreateEvent(Common.GetValidEvent());

            Assert.Equal(3, EventService.GetAll().Count);
        }

        [Fact]
        public void CanGetSpecificEvent()
        {
            var newEvent = EventService.CreateEvent(Common.GetValidEvent());

            Assert.Equal(EventService.GetById(newEvent.Id,false,false).EventDescription, newEvent.EventDescription);
        }

        [Fact]
        public void CantGetMissingEvent()
        {
            EventService.CreateEvent(Common.GetValidEvent());
            Assert.Null(EventService.GetById(Guid.NewGuid().ToString(),false,false));

        }

        [Fact]
        public void CanUpdateExistingEvent()
        {
            var createdEvent = EventService.CreateEvent(Common.GetValidEvent());

            createdEvent.EventDescription = "Updated Event Description";
            EventService.UpdateEvent(createdEvent);

            Assert.Equal("Updated Event Description",EventService .GetById(createdEvent.Id,false,false).EventDescription);
        }

        [Fact]
        public void CantUpdateMissingEvent()
        {
            Assert.Null(EventService.UpdateEvent(new Event() {Id = Guid.NewGuid().ToString()}));
        }
    }

    public class EventRepositoryTests
    {
        private ICrudRepository<Event> EventsRepo { get; set; }

        public EventRepositoryTests()
        {
            EventsRepo = new EventRepository();
        }
        [Fact]
        public void CanCreateEvent()
        {
            var newEvent = EventsRepo.Create(new Event());
            Assert.NotNull(newEvent.Id);
        }
        
        [Fact]
        public void CanDeleteEvent()
        {
            var newEvent = EventsRepo.Create(Common.GetValidEvent());
            EventsRepo.Delete(newEvent.Id);
            Assert.Empty(EventsRepo.GetAll());
        }

        [Fact]
        public void CanGetAllEvents()
        {
            EventsRepo.Create(new Event());
            EventsRepo.Create(new Event());
            EventsRepo.Create(new Event());

            Assert.Equal(3, EventsRepo.GetAll().Count);
        }

        [Fact]
        public void CanGetSpecificEvent()
        {
            var newEvent = EventsRepo.Create(new Event(){EventDescription = "New Event Description"});

            Assert.Equal(EventsRepo.GetById(newEvent.Id).EventDescription, newEvent.EventDescription);
        }

        [Fact]
        public void CantGetMissingEvent()
        {
             EventsRepo.Create(new Event());
             Assert.Null(EventsRepo.GetById(Guid.NewGuid().ToString()));

        }

        [Fact]
        public void CanUpdateExistingEvent()
        {
            var createdEvent = EventsRepo.Create(new Event(){EventDescription = "New Event Desc"});

            createdEvent.EventDescription = "Updated Event Description";
            EventsRepo.Update(createdEvent);

            Assert.Equal("Updated Event Description", EventsRepo.GetById(createdEvent.Id).EventDescription);
        }

        [Fact]
        public void CantUpdateMissingEvent()
        {
            Assert.Null(EventsRepo.Update(new Event() {Id = Guid.NewGuid().ToString()}));
        }
    }
}