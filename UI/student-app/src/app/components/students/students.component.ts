import { Component } from '@angular/core';
import { Subscription } from 'rxjs';
import { IStudent, StudentService } from 'src/app/services/student.service';

@Component({
  selector: 'students',
  templateUrl: './students.component.html',
})
export class StudentsComponent {

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
    var studentSubscription = this.studentService.getStudents().subscribe(response => {
      this.students = response.students;
    });
    this.subscription.add(studentSubscription);
  }
}
