import { Component, inject, OnInit } from "@angular/core";
import { AuthService } from "../../features/auth/auth.service";
import { Router } from "@angular/router";
import { LOGIN_PATH } from "./auth.constants";


@Component({
    selector: 'app-authorization',
    template:``,
})
export class AuthorizationComponent implements OnInit {

    private router: Router = inject(Router);
    protected authService: AuthService = inject(AuthService);

    ngOnInit() {
       this.authService.isAuthenticated().subscribe((isAuthenticated: boolean) => {     
            if (!isAuthenticated) {
                window.location.href = LOGIN_PATH;
            }
        });
    }

    logOut(){
        this.authService.logout();
    }
}