import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  ValidationErrors,
  Validators,
} from '@angular/forms';
import { Recipe } from 'src/app/interfaces/recipe';
import { ImageService } from 'src/app/services/image-service.service';
import { ImagesService } from 'src/app/services/images.service';
import { MenuService } from 'src/app/services/menu.service';
import { RecipeService } from 'src/app/services/recipe.service';
import { environment } from 'src/environments/environment.dev';

@Component({
  selector: 'app-create-recipe',
  templateUrl: './create-recipe.component.html',
  styleUrls: ['./create-recipe.component.css'],
})
export class CreateRecipeComponent implements OnInit {
  RecipeForm: FormGroup;
  Menus: { id: number; title: string }[] = [];
  selectedMenuId!: Number;
  selectedImages: string[] = [];
  selectedImage: string = '';
  private apiPort = environment.apiPort;
  receipeObj: Recipe = {
    id: 0,
    name: '',
    rate: 0,
    restaurantName: '',
    description: '',
    menuName: '',
    price: 0,
    imageUrl: '',
    images: [],
    category: '',
    menuId: 0,
    restaurantId: 0,
  };
  constructor(
    private formBuilder: FormBuilder,
    private imageService: ImageService,
    private recipeService: RecipeService,
    private menuService: MenuService,
    private imagesService: ImagesService
  ) {
    this.RecipeForm = this.formBuilder.group({
      Id: [null, []],
      Name: ['', [Validators.required]],
      Price: [null, [Validators.required, Validators.min(1)]],
      Description: [null, []],
      Menu: new FormControl('', [Validators.required]),
      Image: [
        null,
        [
          Validators.pattern(
            /.*\.(jpg|jpeg|png|gif|bmp|svg|webp|tiff|ico|jfif)$/i
          ),
          Validators.required,
        ],
      ],
      Images: [
        null,
        Validators.pattern(
          /.*\.(jpg|jpeg|png|gif|bmp|svg|webp|tiff|ico|jfif)$/i
        ),
      ],
    });
  }
  ngOnInit() {
    this.menuService.getMenuOfRestaurant().subscribe({
      next: (data) => {
        this.Menus = data;
      },
      error: (e) => {
        console.log(e);
      },
    });
  }

  onSubmit() {
    if (this.RecipeForm.valid) {
      const formData = this.RecipeForm.value;
      formData.Image = this.selectedImage;
      formData.Images = this.selectedImages;

      console.log(formData);
      this.receipeObj.name = formData.Name;
      this.receipeObj.price = formData.Price;
      this.receipeObj.menuId = formData.Menu;
      this.receipeObj.description = formData.Description;
      this.receipeObj.imageUrl = formData.Image;
      this.receipeObj.images = formData.Images;
      this.recipeService.createRecipe(this.receipeObj).subscribe({
        next: (data) => {
          console.log(data);
        },
        error: (e) => {
          console.log(e);
        },
      });
    }
  }

  onMenuChange(event: Event) {
    const selectedId = parseInt((event.target as HTMLSelectElement).value, 10);
    this.selectedMenuId = selectedId;
  }
  onOneImageUpload(event: any) {
    const files: FileList | null = event.target.files;

    if (files && files.length > 0) {
      const file: File = files[0];
      this.imagesService.uploadImage(event.target.files[0]);
      this.selectedImage = `https://localhost:${this.apiPort}/images/${file.name}`;
    }
  }

  onImagesUpload(event: any) {
    const files: FileList | null = event.target.files;
    this.selectedImages = [];
    if (files) {
      for (let image of event.target.files)
          this.imagesService.uploadImage(image);
      for (let i = 0; i < files.length; i++) {
        const file = files.item(i);
        if (file) {
          this.selectedImages.push( `https://localhost:${this.apiPort}/images/${file.name}`);
        }
      }
    }
  }
}
