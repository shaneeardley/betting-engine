import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {EngineEvent} from './classes/event';
import {Market} from './classes/market';
import {EventOutcome} from './classes/event-outcome';
import {MarketOutcome} from './classes/market-outcome';
import {Bet} from './classes/bet';

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

  /// Market Calls

  getMarkets(): Observable<Market[]> {
    return this.http.get<Market[]>(this.marketUrl);
  }

  getMarket(marketId: string): Observable<Market> {
    return this.http.get<Market>(`${this.marketUrl}/${marketId}`);
  }

  getMarketsForEvent(eventId: string): Observable<Market[]> {
    return this.http.get<Market[]>(`${this.marketUrl}/byEventId/${eventId}`);
  }

  createMarket(market: Market): Observable<Market> {
    return this.http.post<Market>(this.marketUrl, market);
  }

  updateMarket(market: Market): Observable<Market> {
    return this.http.put<Market>(this.marketUrl, market);
  }

  deleteMarket(marketId: string): Observable<void> {
    return this.http.delete<void>(`${this.marketUrl}/${marketId}`);
  }

  getMarketOutcome(marketId: string): Observable<MarketOutcome> {
    return this.http.get <MarketOutcome>(`${this.marketUrl}/getMarketOutcome/${marketId}`);
  }

  // Bet Methods

  getBet(betId: string): Observable<Bet> {
    return this.http.get<Bet>(`${this.betUrl}/${betId}`);
  }

  createBet(bet: Bet): Observable<Bet> {
    return this.http.post<Bet>(this.betUrl, bet);
  }

}
