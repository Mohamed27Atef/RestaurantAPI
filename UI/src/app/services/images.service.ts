import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ImagesService {

  constructor(private http: HttpClient) { }

  uploadImage(file: File) {
    console.log(file)
    const formData: FormData = new FormData();
    formData.append('image', file, file.name);
    this.http.put('https://localhost:44397/api/images', formData)
      .subscribe({
        next: response => {
          console.log(response);
        },
        error: error => {
          console.error(error);
        },
      });
  }
}