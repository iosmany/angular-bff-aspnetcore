import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { toObservable } from '@angular/core/rxjs-interop';
import { inject, Injectable, signal } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, map, shareReplay, switchMap, tap } from 'rxjs/operators';
import { UserClaim } from './auth.models';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  readonly http: HttpClient = inject(HttpClient);
  private httpOptions = { headers: new HttpHeaders({ 'X-CSRF': '1' }) }; //this is a fake header, replace it with your own. it's used to enforce a checking on server side

  nameClaim: string = '';
  roleClaim: string = '';

  private _isAuthenticated = signal(false);
  private _isAuthenticated$ = toObservable(this._isAuthenticated).pipe(
    tap(() => {
      debugger;
      this.authenticationChecked.set(new Date());
      console.log('authenticationChecked:', this.authenticationChecked());
    })
  );

  private authenticationChecked = signal<null | Date>(null);

  private userClaims$ = this.http
    .get<UserClaim[]>('/account/user?slide=false', this.httpOptions)
    .pipe(
      map((response: UserClaim[]) => this.handleClaimsResponse(response)),
      catchError((error: HttpErrorResponse) => of(this.handleError(error)))
    );

  handleClaimsResponse(claims: UserClaim[]) {
    this.nameClaim = claims.find((c) => c.type === 'name')?.value || '';
    this.roleClaim = claims.find((c) => c.type === 'role')?.value || '';
    this._isAuthenticated.set(claims.length > 0);
    return claims;
  }

  handleError(error: HttpErrorResponse) : UserClaim[] {
    console.error(error);
    this._isAuthenticated.set(false);
    return [];
  }

  loadUserClaims() {
    return this.userClaims$;
  }

  isAuthenticated(): Observable<boolean> {
    if(this._isAuthenticated()){
      return of(true);
    }
    return this.userClaims$.pipe(
      map((response) => this._isAuthenticated()),
    );
  }
}
