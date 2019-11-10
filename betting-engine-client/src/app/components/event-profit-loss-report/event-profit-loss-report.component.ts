import {Component, OnInit} from '@angular/core';
import {EngineService} from '../../engine.service';
import {ActivatedRoute, Router} from '@angular/router';
import {EngineEvent} from '../../classes/event';
import {EventOutcome} from '../../classes/event-outcome';
import {Market} from '../../classes/market';

@Component({
  selector: 'app-event-profit-loss-report',
  templateUrl: './event-profit-loss-report.component.html',
  styleUrls: ['./event-profit-loss-report.component.scss']
})
export class EventProfitLossReportComponent implements OnInit {

  eventId = '';
  selectedEvent: EngineEvent;
  eventOutcomesList: EventOutcome[] = [];

  constructor(private engineService: EngineService, private router: Router, private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.route.params.subscribe(rPar => {
      this.eventId = rPar.eventId;
      this.engineService.getEvent(this.eventId).subscribe((res: EngineEvent) => {
        this.selectedEvent = res;
        this.loadPlList();
      });
    });
  }

  getMarketOdds(market: Market) {
    let marketOdds = 0;
    if (Market && market.marketProbability) {
      marketOdds = 1 / market.marketProbability;
    }
    return marketOdds;
  }

  loadPlList() {
    if (!this.selectedEvent || !this.selectedEvent.eventMarkets) {
      return;
    }
    this.selectedEvent.eventMarkets.forEach(market => {
      this.engineService.getEventOutcomeForMarket(this.eventId, market.id).subscribe(mRes => {
        this.eventOutcomesList.push(mRes);
      });
    });
  }

  back() {
    this.router.navigate(['event', this.eventId]);
  }

  getMarketBetsTotal(outCome: EventOutcome) {
    return this.eventOutcomesList[0].possibleMarketOutcomes.find(f => f.market.id === outCome.winningMarketId).marketLoseProfitAmount;
  }

  getTotalPlacedBetValue() {
    if (!this.eventOutcomesList || this.eventOutcomesList.length === 0) {
      return 0;
    }

    let total = 0;
    this.eventOutcomesList[0].possibleMarketOutcomes.forEach(out => total += out.marketLoseProfitAmount);
    return total;
  }
}
