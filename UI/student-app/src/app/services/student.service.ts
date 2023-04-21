import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { environment } from "src/environments/environment";

@Injectable()
export class StudentService {

  constructor(private readonly http: HttpClient) {
  }

  getStudents() {
    return this.http.get<IStudentReponse>(`${environment.apiUrl}data`);
  }
}

export interface IStudent {
  name: string;
  age: number;
  hobbies: string[];
}

export interface IStudentReponse {
  students: IStudent[];
}
