import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { LoadingComponent, LoadingService } from './loading.interface'
import { NgbProgressbarModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  imports: [CommonModule, NgbProgressbarModule],
  declarations: [LoadingComponent],
  exports: [LoadingComponent],
  providers: [LoadingService]
})
export class LoadingModule {
  static forRoot() {
    return {
      ngModule: LoadingModule
    };
  }
}
