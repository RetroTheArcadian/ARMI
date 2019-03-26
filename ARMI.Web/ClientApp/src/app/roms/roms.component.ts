import { Component, OnInit, HostBinding } from '@angular/core';
import { RomsService } from '../services/roms.service';
import { Rom } from '../interfaces/_Models';
import { slideInDownAnimation } from '../utils/animation.utils';

@Component({
  selector: 'app-roms',
  templateUrl: './roms.component.html',
  animations: [slideInDownAnimation],
})
export class RomsComponent implements OnInit {
  @HostBinding('@routeAnimation') routeAnimation = true;
  @HostBinding('style.display') display = 'block';
  constructor(public romservice: RomsService)
  {

  }
  public roms: Rom[];
  ngOnInit(): void {
    this.romservice.roms().subscribe(roms => {
      this.roms = roms;
    });
  }

}
