﻿using System.Collections.Generic;
using BettingEngineServer.Classes;
using BettingEngineServer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BettingEngineServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {

        public EventController(IEventService eventService)
        {
            EventService = eventService;
        }

        private IEventService EventService { get; }

        private readonly ILogger<EventController> _logger;

        [HttpGet("{id}")]
        public Event Get(string id)
        {
            return EventService.GetById(id,true,false);
        }

        [HttpGet("getWithAllChildren/{id}")]
        public Event GetWithAllChildren(string id)
        {
            return EventService.GetById(id,true,true);
        }
        
        [HttpGet]
        public List<Event> Get()
        {
            return EventService.GetAll();
        }
        
        [HttpPost]
        public Event Post(Event newEvent)
        {
            return EventService.CreateEvent(newEvent);
        }

        [HttpPut]
        public Event Put(Event updateEvent)
        {
            return EventService.UpdateEvent(updateEvent);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            EventService.DeleteEvent(id);
        }

        [HttpGet("getEventOutcomeForMarket/{eventId}/{marketId}")]
        public EventOutcome GetEventOutcomeForMarket(string eventId, string marketId)
        {
            return EventService.GetEventOutcomeForMarket(eventId, marketId);
        }
    }
} 