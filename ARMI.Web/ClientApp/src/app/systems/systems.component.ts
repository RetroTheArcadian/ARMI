import { Component, OnInit, HostBinding, ViewChild } from '@angular/core';
import { DomSanitizer, SafeStyle } from '@angular/platform-browser';
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
  constructor(public systemService: SystemService, private sanitizer: DomSanitizer)
  {

  }
  config: SwiperConfigInterface = {
    //a11y: true,
    //effect: 'coverflow',
    //centeredSlides: true,
    //coverflowEffect: {
    //  rotate: 50,
    //  stretch: 0,
    //  depth: 100,
    //  modifier: 1,
    //  slideShadows: true,
    //},
    //pagination: {
    //  el: '.swiper-pagination',
    //},
    //direction: 'vertical',
    //slidesPerView: 'auto',
    keyboard: true,
    mousewheel: true,
    scrollbar: true,
    //navigation: false,
    //grabCursor: true,
    //spaceBetween: 4,
    freeMode: true,
    freeModeSticky: true,
    direction: 'vertical',
    slidesPerView: 5,
    spaceBetween: 3,
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
  selectedSystemImage: SafeStyle;
  

  onIndexChange(index: number): void {
    this.selectedSystem = this.systems[index];
    this.selectedSystemImage = this.sanitizer.bypassSecurityTrustStyle("background: url('assets/systems/" + this.selectedSystem.romFolder + ".png') no-repeat right bottom fixed");
    console.log(this.selectedSystemImage);
    //console.log('Swiper index: ', index, this.systems[index]);
    //console.log(this.directiveRef);
  }
}
