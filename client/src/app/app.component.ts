import { AccountService } from './services/account.service';
import { HttpClient, HttpClientModule } from '@angular/common/http';

import { Component, OnInit } from '@angular/core';
import { User } from './models/User';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'client';

  model: any;
  /**
   *
   */
  constructor( private accountService : AccountService) {
    
  }
  ngOnInit() {

    this.setCurrentUser();



  }

  setCurrentUser(){
    
    const user: User = JSON.parse(localStorage.getItem('User'));
    this.accountService.setCurrentUser(user);
  }



}
