import { Component, OnInit } from '@angular/core';
import { ProfileInfo } from 'src/app/interfaces/profile-info';
import { ImagesService } from 'src/app/services/images.service';
import { ProfileService } from 'src/app/services/profile.service';
import { setCookie } from 'typescript-cookie';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
})
export class ProfileComponent implements OnInit {
  apiPort: any;


  constructor(private profileService: ProfileService, private imageService: ImagesService){}
  ngOnInit(): void {
    this.profileService.getUserInfo().subscribe({
      next: data => this.profileInfo = data
    })
  }


  profileInfo: ProfileInfo = {
    userName: '',
    firstName: '',
    lastName : '',
    email : '',
    location: '',
    PhoneNumber : '',
    profileImage : '',
  };
  profile: boolean = true;
  profileSecurity: boolean = false;
  openProfile() {
    this.profile = true;
    this.profileSecurity = false;
  }
  openProfileSecurity() {
    this.profile = false;
    this.profileSecurity = true;
  }
  profileImageSrc: string = '../../../assets/images/user2.png';
  selectedImage: any;
  onImageSelected(event: any) {
    this.imageService.uploadImage(event.target.files[0]);

    const files: FileList | null = event.target.files;

    if (files && files.length > 0) {
      const file: File = files[0]; // Access the first file from the list
      this.selectedImage = `https://localhost:${this.apiPort}/images/${file.name}`;
    }
  }
}
