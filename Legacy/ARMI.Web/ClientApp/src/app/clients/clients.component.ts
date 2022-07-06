import { Component, OnInit, HostBinding } from '@angular/core';
import { Client, ClientHostType } from '../interfaces/_Models';
import { slideInDownAnimation } from '../utils/animation.utils';
import { ClientService } from '../services/client.service';

@Component({
  selector: 'app-clients',
  templateUrl: './clients.component.html',
  animations: [slideInDownAnimation],
})
export class ClientsComponent implements OnInit {
  @HostBinding('@routeAnimation') routeAnimation = true;
  @HostBinding('style.display') display = 'block';
  constructor(public clientService: ClientService)
  {

  }
  public clients: Client[];
  ngOnInit(): void {
    this.clientService.clients().subscribe(clients => {
      this.clients = clients;
    });
    //console.log(this.clients);
    //this.clients.findIndex(client => client.isMaster === true);
    //this.clients
  }
  getClientHostType(int) {
    return ClientHostType[int]
  }
  onIsMasterChange(clientId: number) { //TODO save in void
    let idx = this.clients.findIndex(c => c.clientId === clientId);
    this.clients.forEach(c => c.isMaster = false);
    this.clients[idx].isMaster = true;
  }
}
