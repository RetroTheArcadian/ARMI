import { Injectable } from '@angular/core';

@Injectable()
export class Configuration {
  public Server: string = window.location.hostname === "localhost" ?
    "https://" + window.location.hostname + ":51743"
    :
    "https://" + window.location.hostname;
}
@Injectable()
export class ColorScheme {
  public primaryColour = '#dd0031';
  public secondaryColour = '#006ddd';
  public tertiaryColour = '#dd0031';
}
