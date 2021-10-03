import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent implements OnInit {
  public forecasts: string[];

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
  }

  ngOnInit() {
    this.http.get('GetArchives')
      .subscribe(
        (archives: string[]) => this.forecasts = archives
      );
  }
}
