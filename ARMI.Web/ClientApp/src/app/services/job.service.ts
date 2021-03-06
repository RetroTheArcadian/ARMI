
// $Classes/Enums/Interfaces(filter)[template][separator]
// filter (optional): Matches the name or full name of the current item. * = match any, wrap in [] to match attributes or prefix with : to match interfaces or base classes.
// template: The template to repeat for each matched item
// separator (optional): A separator template that is placed between all templates e.g. $Properties[public $name: $Type][, ]

// More info: http://frhagn.github.io/Typewriter/

import {Injectable, Inject} from "@angular/core";
import {Http, Response, Headers, RequestOptions, RequestOptionsArgs} from "@angular/http";
import {Observable} from "rxjs";
import { throwError, of } from 'rxjs';
import { catchError,map } from 'rxjs/operators';
import { Configuration } from '../app.constants';
//import { OidcSecurityService } from '../../auth/services/oidc.security.service';
import { ArrayUtilities } from '../utils/array.utils';
// import {parseModel} from '../models/ModelHelper';


import { Job } from '../interfaces/_Models';

@Injectable()
export class JobService {
  private actionUrl: string;
  private headers: Headers;
  private dataType = 'application/json; charset=utf-8';

  constructor(private _http: Http, @Inject('BASE_URL') baseUrl: string) {
    this.actionUrl = baseUrl;
    this.setHeaders();
  }

  private setHeaders() {
    this.headers = new Headers();
    this.headers.append('Content-Type', 'application/json');
    this.headers.append('Accept', 'application/json');
    /*
    const token = this._securityService.getToken();
    if (token !== "") {
      const tokenValue = 'Bearer ' + token;
      this.headers.append('Authorization', tokenValue);
    }
    */
  }



    public getJobs = () : Observable<Job[]> => {
        return this._http.request(this.actionUrl + `api/job/getjobs`, new RequestOptions({
            headers: this.headers,
            method: "get",
            body: ArrayUtilities.serializeCircularToJson(null)
        })).pipe(map(res => (ArrayUtilities.rebuildJsonDotNetObj(<Job[]>res.json()))))
        .pipe(catchError(error => of(`Bad Promise: ${error}`)));
    }
    public getRunningJobs = () : Observable<Job[]> => {
        return this._http.request(this.actionUrl + `api/job/getrunningjobs`, new RequestOptions({
            headers: this.headers,
            method: "get",
            body: ArrayUtilities.serializeCircularToJson(null)
        })).pipe(map(res => (ArrayUtilities.rebuildJsonDotNetObj(<Job[]>res.json()))))
        .pipe(catchError(error => of(`Bad Promise: ${error}`)));
    }

    public cancelJob = (jobId: string) : Observable<string> => {
        return this._http.request(this.actionUrl + `api/job/canceljob?jobId=${encodeURIComponent(jobId)}`, new RequestOptions({
            headers: this.headers,
            method: "get",
            body: ArrayUtilities.serializeCircularToJson(null)
        })).pipe(map(res => (ArrayUtilities.rebuildJsonDotNetObj(<string>res.json()))))
        .pipe(catchError(error => of(`Bad Promise: ${error}`)));
    }
}
