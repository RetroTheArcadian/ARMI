import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FlexLayoutModule } from '@angular/flex-layout';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TreeModule } from 'angular-tree-component';
import { ContextMenuModule } from 'ngx-contextmenu';

import { LoadingModule } from './loading/loading.module';
import { ColorScheme } from './app.constants';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ClientsComponent } from './clients/clients.component';
import { EmulatorsComponent } from './emulators/emulators.component';
import { RomsComponent } from './roms/roms.component';
import { RomListsComponent } from './romlists/romlists.component';
import { SystemsComponent } from './systems/systems.component';

/* Data service */
import { ClientService } from './services/client.service';
import { EmulatorService } from './services/emulators.service';
import { ImportService } from './services/import.service';
import { JobHubService } from './services/jobHub.service';
import { RomListService } from './services/romlist.service';
import { RomsService } from './services/roms.service';
import { JobService } from './services/job.service';
import { SystemService } from './services/system.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ClientsComponent,
    EmulatorsComponent,
    RomsComponent,
    RomListsComponent,
    SystemsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    HttpModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'clients', component: ClientsComponent },
      { path: 'emulators', component: EmulatorsComponent },
      { path: 'roms', component: RomsComponent },
      { path: 'romlists', component: RomListsComponent },
      { path: 'systems', component: SystemsComponent },
    ]),
    NgbModule,
    TreeModule.forRoot(),
    LoadingModule.forRoot(),
    ContextMenuModule.forRoot({
      useBootstrap4: true
    })
  ],
  providers: [
    ClientService,
    EmulatorService,
    JobService,
    JobHubService,
    ImportService,
    RomListService,
    RomsService,
    SystemService,
    ColorScheme
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
