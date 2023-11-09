import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Recipe } from 'src/app/interfaces/recipe';
import { ImageService } from 'src/app/services/image-service.service';
import { MenuService } from 'src/app/services/menu.service';
import { RecipeService } from 'src/app/services/recipe.service';

@Component({
  selector: 'app-update-recipe',
  templateUrl: './update-recipe.component.html',
  styleUrls: ['./update-recipe.component.css']
})
export class UpdateRecipeComponent {
  RecipeForm: FormGroup;
  Menus: { id: number; title: string }[] = [];
  selectedMenuId!: Number;
  selectedImages: string[] = [];
  selectedImage: string = '';
  id!: number;
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
    activeRoute : ActivatedRoute,
    private menuService: MenuService
  ) {
    this.id = activeRoute.snapshot.params['id'];
    this.RecipeForm = this.formBuilder.group({
      Id: [null, []],
      Name: ['', [Validators.required]],
      Price: [null, [Validators.required, Validators.min(1)]],
      Description: [null, []],
      Menu: new FormControl('', [Validators.required]),
    
    });
  }
  ngOnInit() {
    this.recipeService.getRecipe(this.id).subscribe({
      next: (data) => {
        console.log(data);
        if (data) {
          this.RecipeForm.setValue({
            Id: data.id,
            Name: data.name,
            Price: data.price,
            Menu: data.menuId,
            Description: data.description,
            Image: '',
            Images: [],
          });
        }
      },
      error: (e) => {
        console.log(e);
      },
    });
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
      this.receipeObj.id = formData.Id;
      this.receipeObj.name = formData.Name;
      this.receipeObj.price = formData.Price;
      this.receipeObj.menuId = formData.Menu;
      this.receipeObj.description = formData.Description;

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

      this.selectedImage = `assets/images/${file.name}`;
      console.log(`File 1: ${file.name}, Size: ${file.size} bytes`);
    }
  }

  onImagesUpload(event: any) {
    const files: FileList | null = event.target.files;
    this.selectedImages = [];
    if (files) {
      for (let i = 0; i < files.length; i++) {
        const file = files.item(i);

        if (file) {
          this.selectedImages.push(`assets/images/receipe/${file.name}`);
          // console.log(`File ${i + 1}: ${file.name}, Size: ${file.size} bytes`);
        }
      }
    }
  }
}
