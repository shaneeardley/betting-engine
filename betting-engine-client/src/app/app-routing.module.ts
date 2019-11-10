import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {EventListComponent} from './components/event-list/event-list.component';
import {EventComponent} from './components/event/event.component';
import {MarketComponent} from './components/market/market.component';
import {BetComponent} from './components/bet/bet.component';


const routes: Routes = [
    {path: 'events', component: EventListComponent, pathMatch: 'full'},
    {path: 'event/:eventId', component: EventComponent, pathMatch: 'full'},
    {path: 'event/:eventId/market/:marketId', component: MarketComponent, pathMatch: 'full'},
    {path: 'event/:eventId/market/:marketId/bet/:betId', component: BetComponent, pathMatch: 'full'}
  ]
;

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
