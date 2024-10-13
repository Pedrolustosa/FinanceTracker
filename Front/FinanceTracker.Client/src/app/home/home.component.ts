import { Component } from '@angular/core';
import { CarouselConfig, CarouselModule } from 'ngx-bootstrap/carousel';
import { NgFor, NgIf } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [NgFor, NgIf, CarouselModule, RouterModule],
  providers: [
    {
      provide: CarouselConfig,
      useValue: { interval: 12000, noPause: true, showIndicators: true },
    },
  ],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent {}
