import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  constructor(private readonly http: HttpClient) {
  }

  getStudents() {
    return this.http.get<IStudent[]>(`${environment.apiUrl}data`);
  }
}

export interface IStudent {
  name: string;
  age: number;
  hobbies: string[];
}
