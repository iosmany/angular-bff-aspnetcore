import { HttpClient, HttpHeaders } from "@angular/common/http";
import { inject, Injectable, signal } from "@angular/core";
import { Observable } from "rxjs";
import { catchError, shareReplay, switchMap } from "rxjs/operators";
import { UserClaim } from "./auth.models";


@Injectable({
    providedIn: 'root'
})
export class AuthService {

  readonly http: HttpClient = inject(HttpClient);
  private httpOptions = { headers: new HttpHeaders({ 'X-CSRF': '1', }), }; //this is a fake header, replace it with your own. it's used to enforce a checking on server side

  private isAuthenticated = signal(false);

  private userClaims$ = this.http.get<UserClaim[]>('/account/user?slide=false', this.httpOptions)
    .pipe(
      catchError((error: any) => {
        console.error(error);
        return [];
      }),
    );



  getUserClaims(): Observable<UserClaim[]> {
    return this.userClaims$;
  }

  
}