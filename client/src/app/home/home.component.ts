import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  registerMode= false;
  constructor() { }

  ngOnInit() {
  }

  toggleRegisterMode(){
    this.registerMode = !this.registerMode;
  }




  cancelRegisterForm(event : boolean){
    this.registerMode = event;
  }
}
