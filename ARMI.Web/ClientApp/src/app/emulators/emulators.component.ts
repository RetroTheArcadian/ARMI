import { Component, OnInit, HostBinding, ViewChild, TemplateRef } from '@angular/core';
import { EmulatorService } from '../services/emulators.service';
import { ImportService } from '../services/import.service';
import { JobService } from '../services/job.service';
import { JobHubService } from '../services/jobHub.service';
import { Emulator, ProgressDto, JobStatus } from '../interfaces/_Models';
import { slideInDownAnimation } from '../utils/animation.utils';
import { ColorScheme } from '../app.constants';
import { LoadingService } from '../loading/loading.interface';

@Component({
  selector: 'app-emulators',
  templateUrl: './emulators.component.html',
  animations: [slideInDownAnimation],
})
export class EmulatorsComponent implements OnInit {
  @HostBinding('@routeAnimation') routeAnimation = true;
  @HostBinding('style.display') display = 'block';

  constructor(
    public emulatorService: EmulatorService,
    public importService: ImportService,
    public jobService: JobService,
    public jobHubService: JobHubService,
    public loadingService: LoadingService,
    public colorScheme: ColorScheme
  )
  {

  }

  emulators: Emulator[] = [];
  ngOnInit(): void {
    this.getEmulators();
  }
  getEmulators() {
    this.loadingService.show();
    this.emulatorService.emulators().subscribe(emulators => {
      this.emulators = emulators;
      this.loadingService.hide();
    });
  }
  sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }
  importEmulators() {
    this.emulators = [];
    this.loadingService.show();
    this.importService.importEmulators().subscribe(jobIdGuid => {
      this.jobHubService.watchJob(jobIdGuid).subscribe(progress => {
        this.loadingService.update(progress);
        console.log("progress", progress);    
        if (progress.jobStatus != JobStatus.running) {          
          this.getEmulators();
          this.loadingService.hide();
          this.updateRunningJobs();
        }
      });
      this.sleep(1000).then(() => {
        this.updateRunningJobs();
      })
    });
  }
  updateRunningJobs() {
    this.jobService.getRunningJobs().subscribe(jobs => {
      this.jobHubService.setRunningJobs(jobs).subscribe(runningJobs => {
        console.log("running jobs", runningJobs);
      })
    });
  }
}
