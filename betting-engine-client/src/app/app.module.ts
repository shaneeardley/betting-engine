import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EventListComponent } from './components/event-list/event-list.component';
import { EventComponent } from './components/event/event.component';
import { MarketComponent } from './components/market/market.component';
import { BetComponent } from './components/bet/bet.component';
import {EngineService} from './engine.service';
import {HttpClientModule} from '@angular/common/http';
import {AngularFontAwesomeModule} from 'angular-font-awesome';
import {FormsModule} from '@angular/forms';
import { EventProfitLossReportComponent } from './components/event-profit-loss-report/event-profit-loss-report.component';

@NgModule({
  declarations: [
    AppComponent,
    EventListComponent,
    EventComponent,
    MarketComponent,
    BetComponent,
    EventProfitLossReportComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    AngularFontAwesomeModule,
    NgbModule,
    FormsModule
  ],
  providers: [EngineService],
  bootstrap: [AppComponent]
})
export class AppModule { }
