import {Component, Inject, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {AuthenticationService} from "../_services/authentication.service";
import {Router} from "@angular/router";
import {first} from "rxjs/operators";

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {
  user: any = {}
  error: any = {}

  constructor(private authService: AuthenticationService, private router: Router) {
    if (this.authService.currentUserValue) {
      this.router.navigate(['/']);
    }
  }

  ngOnInit() {
  }

  authorize() {
    this.authService.login(this.user.login, this.user.password).pipe(first())
      .subscribe(data => {
        this.router.navigate(['/']);
      }, error => {
        this.error = error;
      })
  }
}
