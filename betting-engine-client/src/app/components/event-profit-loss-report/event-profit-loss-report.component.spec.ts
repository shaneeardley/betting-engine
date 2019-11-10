import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EventProfitLossReportComponent } from './event-profit-loss-report.component';

describe('EventProfitLossReportComponent', () => {
  let component: EventProfitLossReportComponent;
  let fixture: ComponentFixture<EventProfitLossReportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EventProfitLossReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EventProfitLossReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
