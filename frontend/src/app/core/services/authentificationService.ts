import { HttpClient, HttpResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Observable, tap } from "rxjs";
import { loginUrl } from "src/app/configs/api-endpoints";
import { IUserLogin } from "../models/loginModel";
import { ILoginResponse } from "../models/loginResponse";

@Injectable({
    providedIn: 'root'
})

export class AuthenticationService {
    
    constructor(private http: HttpClient, private router: Router) { }

    public login(user: IUserLogin): Observable<ILoginResponse>{
        return this.http.post<ILoginResponse>(loginUrl, user)
    }
}
