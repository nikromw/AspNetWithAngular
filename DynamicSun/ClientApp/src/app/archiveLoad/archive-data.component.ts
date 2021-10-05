import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-archive-data',
  templateUrl: './archive-data.component.html'
})
export class ArchiveComponent implements OnInit {
  public archives: string[];
  public selectedArchives: string[] = [];

  constructor(private http: HttpClient, private router: Router, @Inject('BASE_URL') baseUrl: string) {
  }

  selectArchive(archiveName: string) {
    this.selectedArchives.push(archiveName);
  }

  loadArchive() {
    let archives = this.selectedArchives;
    this.router.navigate(['/weather'], { queryParams: { archives } });
  }

  ngOnInit() {
    this.http.get('GetArchives')
      .subscribe(
        (archives: string[]) => this.archives = archives
      );
  }
}
