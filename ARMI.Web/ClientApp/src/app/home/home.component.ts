import { Component, HostBinding } from '@angular/core';
import { slideInDownAnimation } from '../utils/animation.utils';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  animations: [slideInDownAnimation],
})
export class HomeComponent {
  @HostBinding('@routeAnimation') routeAnimation = true;
  @HostBinding('style.display') display = 'block';
}
