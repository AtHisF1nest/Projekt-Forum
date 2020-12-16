import {Component} from '@angular/core';
import {Router} from "@angular/router";
import {AuthenticationService} from "./_services/authentication.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';
  currentUser: User;

  constructor(private router: Router,
              private authService: AuthenticationService) {
    this.authService.currentUser.subscribe(value => this.currentUser = value);
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/logowanie']);
  }
}
