import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent implements OnInit {
  public forecasts: string[];

  constructor(private http: HttpClient, private router: Router, @Inject('BASE_URL') baseUrl: string) {
  }

  loadArchive(archiveName: string) {
    this.router.navigate(['/counter', archiveName]);
  }

  ngOnInit() {
    this.http.get('GetArchives')
      .subscribe(
        (archives: string[]) => this.forecasts = archives
      );
  }
}
