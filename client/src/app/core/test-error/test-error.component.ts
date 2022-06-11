import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss']
})
export class TestErrorComponent implements OnInit {

   baseUrl = environment.apiUrl;
   validationErrors:any;
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }

 get404Error()
 {
  this.http.get(this.baseUrl + 'products/42').subscribe({  
    next: response => console.log(response),  
    error: err => console.error('An error occurred', err),  
    complete: () => console.log('There are no more vowels.')  
  });
 }

 get500Error()
 {
  this.http.get(this.baseUrl + 'buggy/servererror').subscribe({  
    next: response => console.log(response),  
    error: err => console.error('An error occurred', err),  
    complete: () => console.log('There are no more vowels.')  
  });
 }

 get400Error()
 {
  this.http.get(this.baseUrl + 'buggy/badrequest').subscribe({  
    next: response => console.log(response),  
    error: err => console.error('An error occurred', err),  
    complete: () => console.log('There are no more vowels.')  
  });
 }

 get400ValidationError()
 {
  this.http.get(this.baseUrl + 'products/fourtyTwo').subscribe({  
    next: response => console.log(response),  
    error: err => {
    console.error('An error occurred', err)
    this.validationErrors =  err.errors;
  },  
    complete: () => console.log('There are no more vowels.')  
  });
 }

}
