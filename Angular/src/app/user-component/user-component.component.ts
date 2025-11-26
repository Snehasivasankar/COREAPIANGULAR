import { Component } from '@angular/core';
import { AuthserviceService } from '../authservice.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-component',
  templateUrl: './user-component.component.html',
  styleUrls: ['./user-component.component.css']
})
export class UserComponentComponent {
  logData={uname:'',password:''};
  loginResponse:string='';
  constructor(private authserviceservice:AuthserviceService,private router:Router){}
  onLogin(){
    this.authserviceservice.login(this.logData)
    .subscribe((response: any) => {
        console.log('Response from backend:', response); 
      //response=>{
      //this.loginResponse=response.message
      const uid=response?.userId;
      if(uid==undefined)
      {
        this.loginResponse=response.message
      }
      else
      {
        this.router.navigate(['/user-profile',uid]);
      }
    },
    (error)=>{this.loginResponse=error}
    );
  }
  }


