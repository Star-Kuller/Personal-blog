import { Routes } from '@angular/router';
import {GreeterComponent} from "./Components/greeter/greeter.component";
import {AboutComponent} from "./Components/about/about.component";
import {NotFoundComponent} from "./Components/not-found/not-found.component";
import {PagedListComponent} from "./Components/paged-list/paged-list.component";

export const routes: Routes = [
  { path: '', redirectTo: '/about', pathMatch: 'full' },
  { path: 'test', component: GreeterComponent},
  { path: 'about', component: AboutComponent},
  { path: 'blog', component: PagedListComponent},
  { path: '**', component: NotFoundComponent}
];
