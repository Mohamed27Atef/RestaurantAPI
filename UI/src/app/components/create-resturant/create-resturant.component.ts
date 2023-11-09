import { HttpClient } from '@angular/common/http';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  ValidationErrors,
  Validators,
} from '@angular/forms';
import { Restaurant } from 'src/app/interfaces/restaurant';
import { CategoryService } from 'src/app/services/category.service';
import { HeaderService } from 'src/app/services/header.service';
import { ImagesService } from 'src/app/services/images.service';
import { RestaurantService } from 'src/app/services/restaurant.service';
import { environment } from 'src/environments/environment.dev';

@Component({
  selector: 'app-create-resturant',
  templateUrl: './create-resturant.component.html',
  styleUrls: ['./create-resturant.component.css'],
})
export class CreateResturantComponent implements OnInit {
  @ViewChild('opening') openginHours!: ElementRef;
  @ViewChild('closing') closinHours!: ElementRef;

  closingTimeBeforeOpeningTime: boolean = false;
  private apiPort = environment.apiPort;
  restaurantForm: FormGroup;
  categories: { id: number; title: string }[] = [];
  selectedCategoryId!: number;
  selectedImages: string[] = [];
  selectedImage: string = '';
  resturantObj: Restaurant = {
    id: 0,
    name: '',
    email: '',
    Password: '',
    Description: '',
    address: '',
    phone: '',
    cusinetype: '',
    longitude: 0,
    latitude: 0,
    rate: 0,
    openHours: 0,
    ClosingHours: 0,
    image: '',
    images: [],
    restaurantCategories: [],
  };

  constructor(
    private formBuilder: FormBuilder,
    private Allcategory: CategoryService,
    private resturantServ: RestaurantService,
    private imageService: ImagesService
  ) {
    this.restaurantForm = this.formBuilder.group({
      Name: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      Password: ['', [Validators.required, Validators.minLength(3)]],
      Address: [''],
      Description: [''],
      Cusinetype: ['', [Validators.required]],
      Longitude: [null, [Validators.min(-180), Validators.max(180)]],
      Latitude: [null, [Validators.min(-90), Validators.max(90)]],
      category: new FormControl('', [Validators.required]),
      OpenHours: [null, [Validators.min(0), Validators.max(24)]],
      ClosingHours: [null, [Validators.min(0), Validators.max(24)]],

      Image: [
        '',
        [
          Validators.pattern(
            /.*\.(jpg|jpeg|png|gif|bmp|svg|webp|tiff|ico|jfif)$/i
          ),
          Validators.required,
        ],
      ],
      Images: [
        '',
        Validators.pattern(
          /.*\.(jpg|jpeg|png|gif|bmp|svg|webp|tiff|ico|jfif)$/i
        ),
      ],
      phone: ['', [Validators.pattern(/^\d{11}$/)]],
    });
  }
  ngOnInit() {
    this.Allcategory.getAllCategory().subscribe({
      next: (data) => {
        this.categories = data;
      },
      error: (e) => {},
    });
  }

  onSubmit() {
    console.log('Submit');
    if (this.restaurantForm.valid) {
      // Handle form submission here, for example, send data to the server
      const formData = this.restaurantForm.value;
      formData.Image = this.selectedImage;
      formData.Images = this.selectedImages;

      this.resturantObj.name = formData.Name;
      this.resturantObj.email = formData.email;
      this.resturantObj.Password = formData.Password;
      this.resturantObj.address = formData.Address;
      this.resturantObj.phone = formData.phone;
      this.resturantObj.cusinetype = formData.Cusinetype;
      this.resturantObj.longitude = formData.Longitude;
      this.resturantObj.latitude = formData.Latitude;
      this.resturantObj.Description = formData.Description;

      const openHoursParts = formData.OpenHours.split(':');
      const resOpen = openHoursParts[0] + '.' + openHoursParts[1];
      console.log(resOpen);
      this.resturantObj.openHours = +resOpen;

      const closeHoursParts = formData.ClosingHours.split(':');
      const resclose = closeHoursParts[0] + '.' + closeHoursParts[1];
      this.resturantObj.ClosingHours = +resclose;
      this.resturantObj.image = formData.Image;
      this.resturantObj.images = formData.Images;
      this.resturantObj.restaurantCategories = [];
      this.resturantObj.restaurantCategories.push(
        parseInt(formData.category, 10)
      );

      this.resturantObj.rate = 1;
      console.log(this.resturantObj);
      this.resturantServ.createResturant(this.resturantObj).subscribe({
        next: (data) => {
          console.log(data);
          this.restaurantForm.reset();
        },
        error: (e) => {
          console.log(e);
        },
      });
    }
  }
  selectedFile: any = '';
  imageSrc: any = '';
  onCategoryChange(event: Event) {
    const selectedId = parseInt((event.target as HTMLSelectElement).value, 10);
    this.selectedCategoryId = selectedId;
  }

  onOneImageUpload(event: any) {
    this.imageService.uploadImage(event.target.files[0]);

    const files: FileList | null = event.target.files;

    if (files && files.length > 0) {
      const file: File = files[0]; // Access the first file from the list

      this.selectedImage = `https://localhost:${this.apiPort}/images/${file.name}`;
      
    }
  }

  onImagesUpload(event: any) {
    const files: FileList | null = event.target.files;
    this.selectedImages = [];
    if (files) {
      for (let image of event.target.files)
        this.imageService.uploadImage(image);
      for (let i = 0; i < files.length; i++) {
        const file = files.item(i);

        if (file) {
          // Now, 'file' contains the individual file, and you can work with it as needed.
          this.selectedImages.push(
            `https://localhost:${this.apiPort}/images/${file.name}`
          );
          // console.log(`File ${i + 1}: ${file.name}, Size: ${file.size} bytes`);
        }
      }
    }
  }

  onClosingHoursChange() {
    // this.closingTimeAfterOpeningTimeValidator();
    const openingValue = this.openginHours.nativeElement.value;
    const closingValue = this.closinHours.nativeElement.value;

    if (openingValue && closingValue && openingValue <= closingValue) {
      this.closingTimeBeforeOpeningTime = true;
    }
  }
  convertDecimalToTimeString(decimalValue: number): string {
    // Split the decimal value into hours and minutes
    const hours = Math.floor(decimalValue);
    const minutes = (decimalValue - hours) * 100;

    // Format hours and minutes as a time string
    const timeString = `${hours < 10 ? '0' : ''}${hours}:${
      minutes < 10 ? '0' : ''
    }${Math.round(minutes)}`;
    console.log(timeString);
    return timeString;
  }
}
