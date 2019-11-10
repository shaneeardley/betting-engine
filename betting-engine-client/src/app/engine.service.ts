import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {EngineEvent} from './classes/event';
import {EventOutcome} from './classes/event-outcome';

@Injectable()
export class EngineService {

  serverUrl: string;
  eventUrl = '';
  betUrl = '';
  marketUrl = '';

  constructor(private http: HttpClient) {

  }

  public setupUrls(serverUrl: string) {
    this.serverUrl = serverUrl;
    this.eventUrl = `${serverUrl}/Event`;
    this.betUrl = `${serverUrl}/Bet`;
    this.marketUrl = `${serverUrl}/Market`;
  }

  getAllEvents(): Observable<EngineEvent[]> {
    return this.http.get<EngineEvent[]>(`${this.eventUrl}`);
  }

  DeleteEvent(eventId: string): Observable<void> {
    return this.http.delete<void>(`${this.eventUrl}/${eventId}`);
  }

  getEvent(eventId: string): Observable<EngineEvent> {
    return this.http.get<EngineEvent>(`${this.eventUrl}/${eventId}`);
  }

  getEventAndChildren(eventId: string): Observable<EngineEvent> {
    return this.http.get<EngineEvent>(`${this.eventUrl}/getWithAllChildren/${eventId}`);
  }

  createEvent(event: EngineEvent): Observable<EngineEvent> {
    return this.http.post<EngineEvent>(`${this.eventUrl}`, event);
  }

  updateEvent(event: EngineEvent): Observable<EngineEvent> {
    return this.http.put<EngineEvent>(`${this.eventUrl}`, event);
  }

  getEventOutcomeForMarket(eventId: string, marketId: string): Observable<EventOutcome> {
    return this.http.get<EventOutcome>(`${this.eventUrl}/getEventOutcomeForMarket/${eventId}/${marketId}`);
  }
}
