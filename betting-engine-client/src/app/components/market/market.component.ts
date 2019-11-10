import {Component, OnInit} from '@angular/core';
import {Market} from '../../classes/market';
import {ActivatedRoute, Router} from '@angular/router';
import {EngineService} from '../../engine.service';
import {Bet} from '../../classes/bet';

@Component({
  selector: 'app-market',
  templateUrl: './market.component.html',
  styleUrls: ['./market.component.scss']
})
export class MarketComponent implements OnInit {
  isNewMarket = false;
  selectedMarket: Market;
  marketId = '';
  eventId = '';

  getMarketOddsLabel() {
    let marketOdds = 0;
    if (this.selectedMarket && this.selectedMarket.marketProbability) {
      marketOdds = 1 / this.selectedMarket.marketProbability;
    }
    return `Market Odds : ${marketOdds}`;
  }

  constructor(private router: Router, private route: ActivatedRoute, private  engineService: EngineService) {

  }

  back() {
    this.router.navigate(['event', this.eventId]);
  }

  ngOnInit() {
    this.route.params.subscribe(prms => {
      this.marketId = prms.marketId;
      this.eventId = prms.eventId;
      if (this.marketId === 'create') {
        this.isNewMarket = true;
        this.selectedMarket = new Market();
        this.selectedMarket.eventId = this.eventId;
      } else {
        this.engineService.getMarket(this.marketId).subscribe((res: Market) => {
          this.selectedMarket = res;
        });
      }
    });
  }

  someFieldsMissing() {
    return !this.selectedMarket || !this.selectedMarket.marketDescription || !this.selectedMarket.marketProbability;
  }

  saveMarket() {
    if (this.isNewMarket) {
      this.engineService.createMarket(this.selectedMarket).subscribe(res => {
        this.isNewMarket = false;
        this.marketId = res.id;
        this.selectedMarket = res;
      });
    } else {
      this.engineService.updateMarket(this.selectedMarket).subscribe(res => {
        this.selectedMarket = res;
      });
    }
  }

  getHeaderText() {
    if (this.isNewMarket) {
      return 'Create new Market';
    }
    return 'Market Details';
  }

  validatePress() {
    if (!this.selectedMarket || !this.selectedMarket.marketProbability) {
      return;
    }

    if (this.selectedMarket.marketProbability > 1) {
      this.selectedMarket.marketProbability = 1;
    }
    if (this.selectedMarket.marketProbability < 0) {
      this.selectedMarket.marketProbability = 0;
    }
  }

  addNewBet() {
    this.router.navigate(['event', this.eventId, 'market', this.marketId, 'bet', 'create']);
  }

  viewBet(bet: Bet) {
    this.router.navigate(['event', this.eventId, 'market', this.marketId, 'bet', bet.id]);
  }
}
