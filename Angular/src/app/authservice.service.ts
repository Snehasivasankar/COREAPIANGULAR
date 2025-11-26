import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface UserTab{
  uid:number;
  name:string;
  age:number;
  dddr:string;
  email:string;
  photo:string;
  uname:string;
  password:string;
}
const apiUrl='http://localhost:5203/UserManagement/';
@Injectable({
  providedIn: 'root'
})
export class AuthserviceService {

  constructor(private http:HttpClient) { }
  createUser(user:any,file:File):Observable<any>{
    const url=apiUrl+'inserttab';
    const formData:FormData=new FormData();//fromform
    //properties in the class UserCreateDTO(key)
    formData.append("name",user.name);
    formData.append("age",user.age.toString());
    formData.append("addr",user.addr);
    formData.append("email",user.email);
    formData.append("uname",user.uname);
    formData.append("password",user.password);
    //append image file
    if(file){
      formData.append('path',file);
    }
    return this.http.post(url,formData);
  }
  login(credentials:any):Observable<any>{
    return this.http.post(apiUrl+'logintab',credentials);
  }
  getProfile(id:any):Observable<any>{
    return this.http.get<UserTab>(apiUrl + 'gettabWithId/'+id);
  }
}
