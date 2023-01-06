import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { environment } from 'src/environments/environment';
import SmsResponse from '../responses/sms-response';

@Injectable({
  providedIn: 'root'
})
export class SmsService {

  constructor(private httpClient: HttpClient) { }

  getsmss(): Observable<SmsResponse[]> {
    return this.httpClient.get<SmsResponse[]>(`${environment.apiUrl}/smss`);
  }

  savesms(sms: SmsResponse): Observable<SmsResponse> {
    return this.httpClient.post<SmsResponse>(`${environment.apiUrl}/smss`, sms);
  }

  updatesms(sms: SmsResponse): Observable<SmsResponse> {
    return this.httpClient.put<SmsResponse>(`${environment.apiUrl}/smss`, sms);
  }

  deletesms(smsId: number) {
    return this.httpClient.delete<SmsResponse>(`${environment.apiUrl}/smss/${smsId}`);
  }
  
}