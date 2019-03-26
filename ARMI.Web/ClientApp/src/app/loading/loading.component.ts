import { Component, OnInit, HostBinding } from '@angular/core';
import { ProgressDto, JobStatus } from '../interfaces/_Models';
import { LoadingService } from './loading.service';
import { slideInDownAnimation } from '../utils/animation.utils';
import { NgbProgressbarModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'loading',
  templateUrl: './loading.component.html',
  animations: [slideInDownAnimation],
})

export class LoadingComponent implements OnInit {
  @HostBinding('@routeAnimation') routeAnimation = true;
  @HostBinding('style.display') display = 'block';

  constructor(public loadingService: LoadingService) { }
  progress: ProgressDto = {
    done: 0,
    remaining: 0,
    percent: 0,
    total: 0,
    jobStatus: JobStatus.done,
    msgLine1: "",
    msgLine2: ""
  };
  loading = false;
  ngOnInit(): void {
    this.loadingService.hide();
    this.loadingService.loading.subscribe(loading => {
      this.loading = loading;
    });
    this.loadingService.progressDto.subscribe(progress => {
      this.progress = progress;
    });
  }
}
