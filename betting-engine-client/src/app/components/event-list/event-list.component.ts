import {Component, OnInit} from '@angular/core';
import {EngineService} from '../../engine.service';
import {EngineEvent} from '../../classes/event';
import {Router} from '@angular/router';


@Component({
  selector: 'app-event-list',
  templateUrl: './event-list.component.html',
  styleUrls: ['./event-list.component.scss']
})
export class EventListComponent implements OnInit {

  eventsList: EngineEvent[];

  constructor(private engineService: EngineService, private router: Router) {
  }

  addNewEvent() {
    this.router.navigate(['event', 'create']);
  }

  ngOnInit() {
    this.engineService.getAllEvents().subscribe((res: EngineEvent[]) => {
      this.eventsList = res;
    });
  }

  canDelete(event: EngineEvent) {
    // ToDo - not sure if this should be allowed, disabling
    return false;
  }

  deleteEvent(event: EngineEvent) {
    this.engineService.DeleteEvent(event.id).subscribe(() => {
      this.engineService.getAllEvents().subscribe((res: EngineEvent[]) => {
        this.eventsList = res;
      });
    });
  }

  viewEvent(event: EngineEvent) {
    this.router.navigate(['event', event.id]);
  }
}
