import {Component, Inject, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {AuthenticationService} from "../_services/authentication.service";

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  newPassword: string = '';
  newPassword2: string = '';
  validationMessage: string = '';

  constructor(private http: HttpClient, @Inject("BASE_URL") private baseUrl: string,
              private authService: AuthenticationService) { }

  ngOnInit() {
  }

  changePassword() {
    if (this.newPassword !== this.newPassword2) {
      this.validationMessage = 'Passwords are not identical';
      return;
    }

    this.http.put<Validation>(this.baseUrl + 'auth/' + this.authService.currentUserValue.id, {password: this.newPassword}).subscribe(value => {
      if (value !== null) {
        this.validationMessage = value.message;
      } else {
        this.newPassword = '';
        this.newPassword2 = '';
        this.validationMessage = 'Ok';
      }
    });
  }
}
