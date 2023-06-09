import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../services/account.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

@Output() cancelEventEmmitter:EventEmitter<boolean> = new EventEmitter<boolean>();
model: any={};
  constructor(private accountService : AccountService) { }

  ngOnInit() {
  }

  register(){
    this.accountService.register(this.model).subscribe(response => {
      this.cancel();

    }, error => {
      console.log(error);   
    });
  }

  cancel(){
   this.cancelEventEmmitter.emit(false);
  }

}
