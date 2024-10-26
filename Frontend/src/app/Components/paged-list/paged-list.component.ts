import { Component } from '@angular/core';
import {ArticlesClientService} from "../../Clients/Articles/articles-client.service";

@Component({
  selector: 'app-paged-list',
  standalone: true,
  imports: [],
  templateUrl: './paged-list.component.html',
  styleUrl: './paged-list.component.css'
})
export class PagedListComponent{
  constructor(private _client : ArticlesClientService) {
    _client.getPagedList(0, 15)
      .then(x => console.log(x));
  }

}
