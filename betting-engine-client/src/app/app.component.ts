import {Component, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {EngineService} from './engine.service';
import {Config} from './classes/config';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'betting-engine-client';
  private loaded: boolean;

  constructor(private http: HttpClient, private engine: EngineService) {

  }

  ngOnInit(): void {
    this.http.get('assets/config.json').subscribe((res: Config) => {
      this.engine.setupUrls(res.serverUrl);
      this.loaded = true;
    });
  }

}
