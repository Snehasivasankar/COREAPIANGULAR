import { Component,OnInit} from '@angular/core';
import{ActivatedRoute,Router} from '@angular/router';
import { AuthserviceService } from '../authservice.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit{

profileImage:string='';
userData={uid:0,name:'',age:0,addr:'',email:'',photo:'',uname:'',password:''};
constructor(public rest:AuthserviceService,private route:ActivatedRoute,private router:Router){}
ngOnInit(){
  //Get the userid from the URL
  const uId=this.route.snapshot.paramMap.get('id');
  console.log('User ID from URL:', uId);
  if(uId!==null && uId!==undefined){
    this.rest.getProfile(uId).subscribe(uData=>{
      console.log('Profile data from backend:', uData); 
      this.userData=uData;
      this.profileImage=`data:image/jpeg;base64,${this.userData.photo}`;
    });
  }
  
}
  
}


