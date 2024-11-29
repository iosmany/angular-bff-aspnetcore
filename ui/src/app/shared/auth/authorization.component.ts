import { Component, inject, OnInit } from "@angular/core";
import { AuthService } from "../../features/auth/auth.service";
import { Observable } from "rxjs";
import { UserClaim } from "../../features/auth/auth.models";


@Component({
    selector: 'app-authorization',
    template:``,
})
export class AuthorizationComponent implements OnInit {

    private authService: AuthService = inject(AuthService);

    ngOnInit() {
        this.authService.getUserClaims()
            .subscribe((claims: UserClaim[]) => {
                console.log(claims);
            });
    }

}