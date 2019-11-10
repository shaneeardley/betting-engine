import {Market} from './market';

export class EngineEvent {

  constructor() {
    this.eventMarkets = [];
  }

  id: string;
  startDate: Date;
  endDate: Date;
  eventDescription: string;
  eventMarkets: Market[];
}
