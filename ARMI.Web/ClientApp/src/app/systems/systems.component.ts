import { Component, OnInit, HostBinding, ViewChild } from '@angular/core';
import { System } from '../interfaces/_Models';
import { slideInDownAnimation } from '../utils/animation.utils';
import { SystemService } from '../services/system.service';
import {
  SwiperDirective, SwiperConfigInterface,
  SwiperScrollbarInterface, SwiperPaginationInterface
} from 'ngx-swiper-wrapper';
import { transcode } from 'buffer';


@Component({
  selector: 'app-systems',
  templateUrl: './systems.component.html',
  animations: [slideInDownAnimation],
})
export class SystemsComponent implements OnInit {
  @HostBinding('@routeAnimation') routeAnimation = true;
  @HostBinding('style.display') display = 'block';
  @ViewChild('systemSwiper') directiveRef?: SwiperDirective;
  constructor(public systemService: SystemService)
  {

  }
  config: SwiperConfigInterface = {
    a11y: true,
    direction: 'vertical',
    slidesPerView: 10,
    keyboard: true,
    mousewheel: true,
    scrollbar: false,
    navigation: false,
    pagination: true,
    grabCursor: true,
    spaceBetween: 4
  };
 

  modelViewMode = "wheel";
  indexWheel: 1;
  systems: System[];
  selectedSystem: System;
  ngOnInit(): void {
    this.systemService.systems().subscribe(systems => {
      this.systems = systems;
      this.selectedSystem = this.systems[0];
    });
  }

  onIndexChange(index: number): void {
    this.selectedSystem = this.systems[index];
    //console.log('Swiper index: ', index, this.systems[index]);
    //console.log(this.directiveRef);
  }
}
