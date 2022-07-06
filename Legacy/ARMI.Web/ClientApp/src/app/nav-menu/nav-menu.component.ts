import { Component, OnInit } from '@angular/core';
import { JobHubService } from '../services/jobHub.service';
import { Job } from '../interfaces/_Models';
import { concat } from 'rxjs/operators';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  isJobsRunning = false;
  runningJobs: Job[] = [];
  constructor(public jobHubService: JobHubService) {

  }
  ngOnInit(): void {
    this.jobHubService.runningJobs.subscribe(jobs => {
      console.log("runnings job navmenu", jobs);
      this.isJobsRunning = jobs.length > 0;
    });
  }
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
