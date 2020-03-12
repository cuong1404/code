import { Component, OnInit } from '@angular/core';
import { AuthService } from './_services/auth.service';
import {JwtHelperService} from '@auth0/angular-jwt';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'DatingApp-SPA';
  jwtHelper = new JwtHelperService();
  constructor(private auService: AuthService) {

  }
  
  ngOnInit(): void {
    const token = localStorage.getItem('token');
    if (token) {
      this.auService.decodedToken = this.jwtHelper.decodeToken(token);
    }
  }
}
