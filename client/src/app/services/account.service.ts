import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { stringify } from 'querystring';
import {map} from 'rxjs/operators';
import { User } from '../models/User';
import { ReplaySubject } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = 'http://localhost:5006/api/account/';
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();
  constructor(private http : HttpClient) { }


  login(model: any){
    console.log(model);
    return this.http.post(this.baseUrl + 'login', model).pipe(
      map((response: User) =>{
        const user = response;
        if(user){
          localStorage.setItem('User', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    )
  }

  register(model: any){
    return this.http.post(this.baseUrl + 'register', model).pipe(
      map((response: User) =>{
        const user = response;
        if(user){
          localStorage.setItem('User', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    )
  }

  setCurrentUser(user: User){
    this.currentUserSource.next(user);
  }
  
  logout(){
    localStorage.removeItem('User');
    this.currentUserSource.next(null);
  }
}
