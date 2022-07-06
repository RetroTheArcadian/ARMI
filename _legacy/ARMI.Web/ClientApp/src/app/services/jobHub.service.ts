import { Injectable, Inject } from "@angular/core";
import { Subject, Observable } from "rxjs";
import { HubConnection } from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';
import { ProgressDto, Job, JobStatus } from '../interfaces/_Models';

@Injectable()
export class JobHubService {
  private hubConnection: HubConnection | undefined;
  public progressDto$: Subject<ProgressDto> = new Subject<ProgressDto>();
  public progressDto: Observable<ProgressDto>;
  public runningJobs$: Subject<Job[]> = new Subject<Job[]>();
  public runningJobs: Observable<Job[]>;
  public runningJobsProgress$: Subject<ProgressDto[]> = new Subject<ProgressDto[]>();
  public runningJobsProgress: Observable<ProgressDto[]>;

  constructor(@Inject('BASE_URL') baseUrl: string) {
    this.progressDto = this.progressDto$.asObservable();
    this.runningJobs = this.runningJobs$.asObservable();
    this.runningJobsProgress = this.runningJobsProgress$.asObservable();
    if (!this.hubConnection) {
      this.hubConnection = new signalR.HubConnectionBuilder()
        .withUrl(baseUrl + 'jobprogress')
        .configureLogging(signalR.LogLevel.Information)
        .build();
      this.hubConnection.on('progress', (progressDto: ProgressDto) => {
        //console.log("service progress: ", progressDto);
        this.progressDto$.next(progressDto);
      });
      this.hubConnection.start().catch(err => console.error(err));
    }
  }

  public setRunningJobs(jobs: Job[]) {
    this.runningJobs$.next(jobs);
    var runningJobsProgress: ProgressDto[] = [];
    for (var i = 0; i < jobs.length; i++) {
      this.watchJob(jobs[i].jobIdGuid).subscribe(w => {
        runningJobsProgress.push(w);
      });
    }
    //this.runningJobsProgress$.next(runningJobsProgress);
    //console.log(this.runningJobs);
    //console.log(this.runningJobsProgress);
    return this.runningJobs;
  }
 
  public watchJob = (jobIdGuid: string): Observable<ProgressDto> => {
    this.hubConnection.invoke("AssociateJob", jobIdGuid)
      .catch(err => console.error(err.toString()));
    return this.progressDto;
  }
}
