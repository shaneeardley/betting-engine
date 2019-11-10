import {MarketOutcome} from './market-outcome';
import {Market} from './market';

export class EventOutcome {
  constructor() {
    this.possibleMarketOutcomes = [];
  }

  eventId: string;
  winningMarketId: string;
  winningMarket: Market;
  plAmount: number;
  possibleMarketOutcomes: MarketOutcome[];
}
