import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserregisterComponent } from './userregister/userregister.component';
import { UserComponentComponent } from './user-component/user-component.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
const routes: Routes = [
  {path:'',component:UserregisterComponent},
  {path:'Login',component:UserComponentComponent},
  {path:'insertuser',component:UserregisterComponent},
  {path:'user-profile/:id',component:UserProfileComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
