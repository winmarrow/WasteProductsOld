import { Component, OnInit } from '@angular/core';
import { User } from '../../../../models/users/user';
import { UserService } from '../../../../services/user/user.service';

@Component ({
  selector: 'app-userdata',
  templateUrl: './userdata.component.html',
  styleUrls: ['./userdata.component.css']
})
export class UserdataComponent implements OnInit {

  user: User;
  isConfirmed: string;

  constructor(public userService: UserService) {
   }

  ngOnInit() {
    this.userService.getUserSettings().subscribe(
      res => this.user = res,
      err => console.error(err)
      );
  }
  updateUserNameAndEmail() {
    this.userService.updateUserName(this.user.UserName).subscribe(
      err => console.error(err)
      );

    this.userService.updateEmail(this.user.Email).subscribe(
      err => console.error(err)
      );
  }

}
