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
  showErrorText = false;
  errorText = '';
  selectedEvent: EngineEvent;
  eventId = '';

  constructor(private router: Router, private route: ActivatedRoute, private  engineService: EngineService) {
  }

  ngOnInit() {
    this.route.params.subscribe(prms => {
      this.eventId = prms.eventId;
      this.showErrorText = false;
      this.errorText = '';
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
    this.router.navigate(['event', this.selectedEvent.id, 'market', 'create']);
  }

  viewMarket(market: Market) {
    this.router.navigate(['event', this.selectedEvent.id, 'market', market.id]);
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

  back() {
    this.router.navigate(['events']);
  }

  saveEvent() {
    this.showErrorText = false;
    this.errorText = '';
    if (this.isNewEvent) {
      this.engineService.createEvent(this.selectedEvent).subscribe(res => {
        this.isNewEvent = false;
        this.eventId = res.id;
        this.selectedEvent = res;
      }, ex => {
        this.showErrorText = true;
        if (ex.error && !ex.error.title && ex.error.includes('\n')) {
          this.errorText = ex.error.split('\n')[0];
        } else this.errorText = ex.error.title;
      });
    } else {
      this.engineService.updateEvent(this.selectedEvent).subscribe(res => {
        this.selectedEvent = res;
      }, ex => {
        this.showErrorText = true;
        if (ex.error && !ex.error.title && ex.error.includes('\n')) {
          this.errorText = ex.error.split('\n')[0];
        } else this.errorText = ex.error.title;
      });
    }
  }

  getMarketOdds(market: Market) {
    let marketOdds = 0;
    if (Market && market.marketProbability) {
      marketOdds = 1 / market.marketProbability;
    }
    return marketOdds;
  }

  viewAllMarketOutcomes() {
    this.router.navigate(['event', 'profit-loss-report', this.selectedEvent.id])
  }
}
