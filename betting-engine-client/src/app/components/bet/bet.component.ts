import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {EngineService} from '../../engine.service';
import {Bet} from '../../classes/bet';
import {EngineEvent} from '../../classes/event';

@Component({
  selector: 'app-bet',
  templateUrl: './bet.component.html',
  styleUrls: ['./bet.component.scss']
})
export class BetComponent implements OnInit {
  marketId = '';
  betId = '';
  eventId = '';
  isNewBet = false;
  selectedBet: Bet;

  constructor(private router: Router, private route: ActivatedRoute, private  engineService: EngineService) {
  }

  ngOnInit() {
    this.route.params.subscribe(prms => {
      this.marketId = prms.marketId;
      this.betId = prms.betId;
      this.eventId = prms.eventId;

      if (this.betId === 'create') {
        this.isNewBet = true;
        this.selectedBet = new Bet();
        this.selectedBet.marketId = this.marketId;
      } else {
        this.engineService.getBet(this.betId).subscribe((res: Bet) => {
          this.selectedBet = res;
        });
      }
    });
  }

  someFieldsMissing() {
    return !this.selectedBet || !this.selectedBet.betAmount || !this.isNewBet;
  }

  saveBet() {
    if (this.isNewBet) {
      this.engineService.createBet(this.selectedBet).subscribe(res => {
        this.isNewBet = false;
        this.betId= res.id;
        this.selectedBet = res;
        this.back();
      });
    }
  }

  back() {
    this.router.navigate(['event', this.eventId, 'market', this.marketId]);
  }

  getHeaderText() {
    if (this.isNewBet) {
      return 'Create new Bet';
    }
    return 'Bet Details';
  }
}
