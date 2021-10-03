import { Component, Inject, Input, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html'
})
export class CounterComponent implements OnInit  {
  public weatherArr: Weather[];
  @Input() fromYear: number;
  @Input() toYear: number;
  @Input() fromMonth: number;
  @Input() toMonth: number;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
  }

  ngOnInit() {
    this.http.get('GetAllWeather')
      .subscribe(
        (weather: Weather[]) => {
          this.weatherArr = [];
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
        }
      );
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
