import {Bet} from './bet';

export class Market {
  constructor() {
    this.MarketBets = [];
  }

  Id: string;
  MarketProbability: number;
  MarketDescription: string;
  EventId: string;
  MarketBets: Bet[];

  getMarketOdds() {
    if (this.MarketProbability === 0) {
      return 0;
    }
    return 1 / this.MarketProbability;
  }
}
