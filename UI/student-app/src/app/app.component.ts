import { Component } from '@angular/core';
import { IStudent, StudentService } from './services/student.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  students!: IStudent[];
  private subscription: Subscription = new Subscription();

  constructor(private readonly studentService: StudentService) {
  }

  ngOnInit(): void {
    this.loadStudents();
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }

  private loadStudents() {
    var studentSubscription = this.studentService.getStudents().subscribe(students => {
      this.students = students;
    });
    this.subscription.add(studentSubscription);
  }
}
