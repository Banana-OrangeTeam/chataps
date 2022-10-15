import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { IUserLogin } from 'src/app/core/models/loginModel';
import { AuthenticationService } from 'src/app/core/services/authentificationService';
import { ToastService } from 'src/app/core/services/toast.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  
  userForLogin: IUserLogin;
  loginForm: FormGroup;
  hide = true;

  constructor(private authService: AuthenticationService, 
    private toastService: ToastService,
    private router: Router ) {
    this.loginForm = new FormGroup({
      email: new FormControl(''),
      password: new FormControl(null),
    })
  }

  ngOnInit() {
  }

  onSubmit() {
    this.userForLogin = this.loginForm.value;
    this.authService.login(this.userForLogin).subscribe((data) => {
      if(data){
        localStorage.setItem('token', data.token);
        this.router.navigate(['user_chats']);
        this.toastService.showSuccess("login success", "Login");
      }
    }, (err) => {
      this.toastService.showError(`${err.error.error}`, "Login");
    });
  }
}
