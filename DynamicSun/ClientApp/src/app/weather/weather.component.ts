import { Component, Inject, Input, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-weather-component',
  templateUrl: './weather.component.html'
})
export class WeatherTableComponent implements OnInit {
  public weatherArr: Weather[] = [];
  public archiveName: string[];
  public index: number ;
  @Input() fromYear: number = 0;
  @Input() toYear: number = (new Date()).getFullYear();
  @Input() fromMonth: number = 1;
  @Input() toMonth: number = 12;

  constructor(private http: HttpClient, private router: ActivatedRoute, @Inject('BASE_URL') baseUrl: string) {
    router.queryParams.subscribe(params => this.archiveName = params['archives']);
  }

  getWeatherByFilter(offset: number = 0) {
    if (this.fromYear == undefined
      || this.toYear == undefined
      || this.fromYear < 0
      || this.toYear < 0) {
      alert("Поля фильтра года не заполнены!");
    } else if (this.fromMonth <= 0 || this.toMonth >= 13) {
      alert("Поля фильтра месяца имеют не верные значения!");
    } else {
      this.index += offset;

      if (this.index < 0) {
        this.index = 0;
      }

      this.http.get(`GetWeather/${this.archiveName}/${this.index}?fromYear=${this.fromYear}&toYear=${this.toYear}&fromMonth=${this.fromMonth}&toMonth=${this.toMonth}`)
        .subscribe((weather: Weather[]) => {
          this.weatherArr = [];

          if (weather === null) {
            this.index -= 1;
          }

          for (let w of weather) {
            let tmpWeather = new Weather;

            tmpWeather.archiveName = w.archiveName;
            tmpWeather.date = w.date.split('T')[0];
            tmpWeather.time = w.date.split('T')[1];
            tmpWeather.temp = w.temp;
            tmpWeather.wet = w.wet;
            tmpWeather.dewPoint = w.dewPoint;
            tmpWeather.pressure = w.pressure;
            tmpWeather.windDirect = w.windDirect;
            tmpWeather.windSpeed = w.windSpeed;
            tmpWeather.cloudCover = w.cloudCover;
            tmpWeather.lowLimitCloud = w.lowLimitCloud;
            tmpWeather.horizontalVisibility = w.horizontalVisibility;
            tmpWeather.weatherEffect = w.weatherEffect;

            this.weatherArr.push(tmpWeather);
          }
        })
    }
  }

  ngOnInit() {
    this.index = 0;
    this.getWeatherByFilter(0);
  }
}

export class Weather {
  public id: number;
  public archiveName: string;
  public date: string;
  public time: string;
  public temp: number;
  public wet: number;
  public dewPoint: number;
  public pressure: number;
  public windDirect: string;
  public windSpeed: number;
  public cloudCover: number;
  public lowLimitCloud: number;
  public horizontalVisibility: string;
  public weatherEffect: string;
}
