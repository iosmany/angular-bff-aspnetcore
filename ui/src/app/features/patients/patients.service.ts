import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { resolvePath, Result } from "../../api/utils";
import { Patient } from "./patients.models";
import { catchError, map, of } from "rxjs";


@Injectable({
    providedIn: 'root'
})
export class PatientsService {

    readonly http: HttpClient = inject(HttpClient);
    httpOptions = { headers: new HttpHeaders({ 'X-CSRF': '1', }), };

    getPatients() {
        return this.http.get<Patient[]>(resolvePath('patients'), this.httpOptions)
          .pipe(
            map((response: Patient[]) => {
                const res: Result<Patient[]> = {
                    data: response,
                    error: ''
                } 
                return res;
            }),
            catchError((error: HttpErrorResponse)=>{
                const res: Result<Patient[]> = {
                    data: [],
                    error: error.message
                }
                return of(res);
            })
          )
    }
}