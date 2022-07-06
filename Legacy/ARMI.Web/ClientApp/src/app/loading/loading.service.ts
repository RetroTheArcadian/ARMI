import { Injectable, Inject } from "@angular/core";
import { Subject, Observable } from "rxjs";
import { ProgressDto, JobStatus } from '../interfaces/_Models';

@Injectable()
export class LoadingService {
  public progressDto$: Subject<ProgressDto> = new Subject<ProgressDto>();
  public progressDto: Observable<ProgressDto>;
  public loading$: Subject<boolean> = new Subject<boolean>();
  public loading: Observable<boolean>;
  private progress: ProgressDto = {
    done: 0,
    remaining: 0,
    percent: 0,
    total: 0,
    jobStatus: JobStatus.done,
    msgLine1: "",
    msgLine2: ""
  };
  constructor() {
    this.progressDto = this.progressDto$.asObservable();
    this.loading = this.loading$.asObservable();
  }

  show() {
    this.loading$.next(true);
    this.update(this.progress);
  }
  hide() {
    this.loading$.next(false);
    this.update(this.progress);
  }
  update(progress: ProgressDto) {
    this.progressDto$.next(progress);
  }
}
