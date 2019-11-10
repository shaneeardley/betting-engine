import {Bet} from './bet';

export class Market {
  constructor() {
    this.marketBets = [];
  }

  id: string;
  marketProbability: number;
  marketDescription: string;
  eventId: string;
  marketBets: Bet[];

  public getMarketOdds() {
    if (this.marketProbability === 0) {
      return 0;
    }
    return 1 / this.marketProbability;
  }
}
