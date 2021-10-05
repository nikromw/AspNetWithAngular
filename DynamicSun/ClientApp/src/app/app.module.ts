import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { LoadFiles } from './loadFiles/loadFile.component';
import { ArchiveComponent } from './selectArchives/archive-data.component';
import { WeatherTableComponent } from './weather/weather.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LoadFiles,
    WeatherTableComponent,
    ArchiveComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: LoadFiles, pathMatch: 'full' },
      { path: 'weather', component: WeatherTableComponent },
      { path: 'archive-select', component: ArchiveComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
