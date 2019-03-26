import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TreeModule } from 'angular-tree-component';
import { ContextMenuModule } from 'ngx-contextmenu';
import { SwiperModule } from 'ngx-swiper-wrapper';
import { SWIPER_CONFIG } from 'ngx-swiper-wrapper';
import { SwiperConfigInterface } from 'ngx-swiper-wrapper';
const DEFAULT_SWIPER_CONFIG: SwiperConfigInterface = {
  direction: 'horizontal',
  slidesPerView: 'auto'
};

import { LoadingModule } from './loading/loading.module';
import { ColorScheme } from './app.constants';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ClientsComponent } from './clients/clients.component';
import { EmulatorsComponent } from './emulators/emulators.component';
import { RomsComponent } from './roms/roms.component';
import { RomListsComponent } from './romlists/romlists.component';

/* Data service */
import { ClientService } from './services/client.service';
import { EmulatorService } from './services/emulators.service';
import { ImportService } from './services/import.service';
import { JobHubService } from './services/jobHub.service';
import { RomListService } from './services/romlist.service';
import { RomsService } from './services/roms.service';
import { JobService } from './services/job.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ClientsComponent,
    EmulatorsComponent,
    RomsComponent,
    RomListsComponent
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
      { path: 'romlists', component: RomListsComponent }
    ]),
    NgbModule,
    TreeModule.forRoot(),
    LoadingModule.forRoot(),
    ContextMenuModule.forRoot({
      useBootstrap4: true
    }),
    SwiperModule
  ],
  providers: [
    ClientService,
    EmulatorService,
    JobService,
    JobHubService,
    ImportService,
    RomListService,
    RomsService,
    ColorScheme,
    {
      provide: SWIPER_CONFIG,
      useValue: DEFAULT_SWIPER_CONFIG
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
