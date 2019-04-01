import { Component, OnInit, HostBinding } from '@angular/core';
import { System } from '../interfaces/_Models';
import { slideInDownAnimation } from '../utils/animation.utils';
import { SystemService } from '../services/system.service';
import { concat } from 'rxjs/operators';


@Component({
  selector: 'app-systems',
  templateUrl: './systems.component.html',
  animations: [slideInDownAnimation],
})
export class SystemsComponent implements OnInit {
  @HostBinding('@routeAnimation') routeAnimation = true;
  @HostBinding('style.display') display = 'block';

  constructor(public systemService: SystemService)
  {

  }
  systems: System[];
  selectedSystem: System;
  ngOnInit(): void {
    this.systemService.systems().subscribe(systems => {
      this.systems = systems;
      this.selectedSystem = this.systems[0];
    });
  }  

  onIndexChange(index: number): void {
    console.log("onIndexChange", index)
    this.selectedSystem = this.systems[index];
  }
}
