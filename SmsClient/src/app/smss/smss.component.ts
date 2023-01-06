import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import SmsResponse from '../responses/sms-response';
import { SmsService } from '../services/sms.service';

@Component({
  selector: 'app-smss',
  templateUrl: './smss.component.html',
  styleUrls: ['./smss.component.css']
})
export class SmssComponent implements OnInit {

  newsmsString: string = '';
  smss$: Observable<SmsResponse[]> | undefined;
  constructor(private smsService: SmsService) { }

  ngOnInit(): void {
    this.smss$ = this.smsService.getsmss();
  }

  addsms(): void {
    if (!this.newsmsString){
      return;
    }
    let sms: SmsResponse = {
      id: 0,
      isCompleted: false,
      name: this.newsmsString,
      ts: new Date()
    };
    this.smsService.savesms(sms).subscribe(() => this.smss$ = this.smsService.getsmss());
  }

  updatesms(sms: SmsResponse) {
    console.log('inside updatesms');
    console.log(`sms id is ${sms.id}`);
    sms.isCompleted = !sms.isCompleted;
    this.smsService.updatesms(sms).subscribe(() => {});

  }

  removesms(sms: SmsResponse) {
    console.log('inside removesms');
    console.log(`sms id is ${sms.id}`);
    this.smsService.deletesms(sms.id).subscribe(() => this.smss$ = this.smsService.getsmss());
  }

}
