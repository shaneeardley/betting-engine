import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {EngineService} from '../../engine.service';
import {EngineEvent} from '../../classes/event';
import {Market} from '../../classes/market';

@Component({
  selector: 'app-event',
  templateUrl: './event.component.html',
  styleUrls: ['./event.component.scss']
})
export class EventComponent implements OnInit {

  isNewEvent = false;
  selectedEvent: EngineEvent;
  eventId = '';

  constructor(private router: Router, private route: ActivatedRoute, private  engineService: EngineService) {
  }

  ngOnInit() {
    this.route.params.subscribe(prms => {
      this.eventId = prms.eventId;
      if (this.eventId === 'create') {
        this.isNewEvent = true;
        this.selectedEvent = new EngineEvent();
      } else {
        this.engineService.getEvent(this.eventId).subscribe((res: EngineEvent) => {
          this.selectedEvent = res;
        });
      }
    });
  }

  addNewMarket() {

  }

  viewMarket(market: Market) {

  }

  someFieldsMissing() {
    return !this.selectedEvent || !this.selectedEvent.endDate || !this.selectedEvent.startDate || !this.selectedEvent.eventDescription;
  }

  getHeaderText() {
    if (this.isNewEvent) {
      return 'Create new Event';
    }
    return 'Event Details';
  }

  saveEvent() {
    this.engineService.createEvent(this.selectedEvent).subscribe(res => {
      debugger
      this.isNewEvent = false;
      this.selectedEvent = res;
    });
  }
}
