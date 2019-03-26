${
    // Enable extension methods by adding using Typewriter.Extensions.*
    using Typewriter.Extensions.Types;
    using Typewriter.Extensions.WebApi;

    // Uncomment the constructor to change template settings.
    Template(Settings settings)
    {
        //settings.IncludeProject("Project.Name");
        settings.OutputExtension = ".ts"; //could also be .tsx

        settings.OutputFilenameFactory = file => System.IO.Path.ChangeExtension(RenameControllerToService(file.Name), settings.OutputExtension);
    }

    // Custom extension methods can be used in the template by adding a $ prefix
    string RenameControllerToService(string name) => name.Replace("Controller", ".service").ToLower();
    string RenameToService(string name) => name.Replace("Controller", "Service");
    string RenameControllerApi(string name) => name.Replace("Controller", "").ToLower();
    string ServiceName(Class c) => RenameToService(c.Name);
    string ApiName(Class c) => RenameControllerApi(c.Name);
    Type[] CalculatedModelTypes(Class c)
    {
        var allTypes = c.Methods
            .SelectMany(m => m.Parameters.Select(p => p.Type).Concat(new [] { m.Type }))
            .Select(t => CalculatedType(t))
            .Where(t => t != null && (t.IsDefined || (t.IsEnumerable && !t.IsPrimitive)))
            .ToLookup(t => t.ClassName(), t => t);
        return allTypes.ToDictionary(l => l.Key, l => l.First()).Select(kvp => kvp.Value).ToArray();
    }
    string CalculatedModelTypesImports(Class c){
        string result = string.Empty;
        var allTypes = c.Methods
                .SelectMany(m => m.Parameters.Select(p => p.Type).Concat(new [] { m.Type }))
                .Select(t => CalculatedType(t))
                .Where(t => t != null && (t.IsDefined || (t.IsEnumerable && !t.IsPrimitive)))
                .Select(t => t.ClassName()).Distinct();
        foreach(var type in allTypes){
            var last = allTypes.Last();
            result += last == type ? type : type + ", ";
        }
        return result;
    }
    Type CalculatedType(Type t)
    {
        var type = t;
        while (!type.IsEnumerable && type.IsGeneric) {
            type = type.Unwrap();
        }
        return type.Name == "IHttpActionResult" ? null : type;
    }
    string CalculatedTypeName(Type t)
    {
        var type = CalculatedType(t);
        return type != null ? type.Name : "void";
    }
    string UrlTrimmed(Method m) => m.Url().TrimEnd('/');
}
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

$Classes(c => c.Namespace == "ARMI.Controllers")[
import { $CalculatedModelTypesImports } from '../interfaces/_Models';

@Injectable()
export class $ServiceName {
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

$Methods(m => CalculatedTypeName(m.Type) == "void")[
    public $name = ($Parameters[$name: $Type][, ]) : Observable<Response> => {
        return this._http.request(this.actionUrl + `$UrlTrimmed`, new RequestOptions({
            headers: this.headers,
            method: "$HttpMethod",
            body: ArrayUtilities.serializeCircularToJson($RequestData)
        }));
    }]
$Methods(m => CalculatedTypeName(m.Type) != "void" && !CalculatedType(m.Type).IsPrimitive)[
    public $name = ($Parameters[$name: $Type][, ]) : Observable<$Type[$CalculatedTypeName]> => {
        return this._http.request(this.actionUrl + `$UrlTrimmed`, new RequestOptions({
            headers: this.headers,
            method: "$HttpMethod",
            body: ArrayUtilities.serializeCircularToJson($RequestData)
        })).pipe(map(res => (ArrayUtilities.rebuildJsonDotNetObj(<$Type[$CalculatedTypeName]>res.json()))))
        .pipe(catchError(error => of(`Bad Promise: ${error}`)));
    }]
$Methods(m => CalculatedTypeName(m.Type) != "void" && CalculatedType(m.Type).IsPrimitive)[
    public $name = ($Parameters[$name: $Type][, ]) : Observable<$Type[$CalculatedTypeName]> => {
        return this._http.request(this.actionUrl + `$UrlTrimmed`, new RequestOptions({
            headers: this.headers,
            method: "$HttpMethod",
            body: ArrayUtilities.serializeCircularToJson($RequestData)
        })).pipe(map(res => (ArrayUtilities.rebuildJsonDotNetObj(<$Type[$CalculatedTypeName]>res.json()))))
        .pipe(catchError(error => of(`Bad Promise: ${error}`)));
    }]
}]
