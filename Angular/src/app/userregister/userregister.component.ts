import { Component,OnInit } from '@angular/core';

import { AuthserviceService } from '../authservice.service';
 

@Component({
  selector: 'app-userregister',
  templateUrl: './userregister.component.html',
  styleUrls: ['./userregister.component.css']
})
export class UserregisterComponent implements OnInit {
userData={name:'',age:0,addr:'',email:'',uname:'',password:''};
selectedFile:File|null=null;
RegResponse:string='';
constructor(public rest:AuthserviceService){}
ngOnInit(): void {
  
}
adduserdata():void{
  if(this.selectedFile){
    this.rest.createUser(this.userData,this.selectedFile)
    .subscribe(response=>{
      this.RegResponse=response.message;
      //console.log("User created successfully",response);
    });
  }
}
onFileSelected(event:any):void{
  this.selectedFile=event.target.files[0];
}

}
