import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http'
import { Observable, throwError } from 'rxjs'
import { IAllAccount } from '../interface/IAllAccount'
import { tap, catchError } from 'rxjs/operators'
import { Injectable } from '@angular/core'
@Injectable()
export class AccountService{

    constructor(private http:HttpClient){}
    LoadAllAccount(pageSize = 6,PageIndex = 1)  :Observable<IAllAccount[]>{
        const   headers = {
               headers: new HttpHeaders({
               Authorization: `Bearer ${JSON.parse(localStorage.getItem("access_token"))}`
           })
           }
           return this.http.get<IAllAccount[]>(`http://localhost:5000/api/account?${pageSize}&${PageIndex}`, headers)
           .pipe(
               tap(data => console.log('All: ' + JSON.stringify(data))),
               catchError(this.handleError))
           
    }

    private handleError(err: HttpErrorResponse) {
        // in a real world app, we may send the server to some remote logging infrastructure
        // instead of just logging it to the console
        let errorMessage = '';
        if (err.error instanceof ErrorEvent) {
          // A client-side or network error occurred. Handle it accordingly.
          errorMessage = `An error occurred: ${err.error.message}`;
        } else {
          // The backend returned an unsuccessful response code.
          // The response body may contain clues as to what went wrong,
          errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
        }
        console.error(errorMessage);
        return throwError(errorMessage);
      }
    
    
}