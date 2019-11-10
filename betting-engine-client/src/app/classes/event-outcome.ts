import {MarketOutcome} from './market-outcome';

export class EventOutcome {
  constructor() {
    this.PossibleMarketOutcomes = [];
  }

  EventId: string;
  WinningMarketId: string;
  PLAmount: number;
  PossibleMarketOutcomes: MarketOutcome[];
}
