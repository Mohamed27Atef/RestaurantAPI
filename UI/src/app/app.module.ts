import { NgModule } from '@angular/core';
import { BrowserModule,HammerModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { NgxStarRatingModule } from 'ngx-star-rating-latest';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CarouselModule } from 'ngx-owl-carousel-o';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { ErrorComponent } from './components/error/error.component';
import { RecipeDetailsComponent } from './components/recipe-details/recipe-details.component';
import { OrderDetailsComponent } from './components/order-details/order-details.component';
import { ShoppingCartComponent } from './components/shopping-cart/shopping-cart.component';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { FeedbackComponent } from './components/feedback/feedback.component';
import { SliderComponent } from './components/slider/slider.component';
import { RestaurantOrdersComponent } from './components/restaurant-orders/restaurant-orders.component';
import { NavbarComponent } from './components/header/navbar/navbar.component';
import { MostPopularComponent } from './components/most-popular/most-popular.component';
import { SearchComponent } from './components/search/search.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RestaurantComponent } from './components/restaurant/restaurant.component';
import { ImagesComponent } from './components/restaurant/images/images.component';
import { InfoComponent } from './components/restaurant/info/info.component';
import { OffersComponent } from './components/restaurant/offers/offers.component';
import { ReservationsComponent } from './components/restaurant/reservations/reservations.component';
import { ContactComponent } from './components/restaurant/contact/contact.component';
import { MenuComponent } from './components/restaurant/menu/menu.component';
import { CommonModule, DatePipe } from '@angular/common';
import { RestaurantOwlCarouselComponent } from './components/restaurant-owl-carousel/restaurant-owl-carousel.component';
import { GetLocationComponent } from './components/get-location/get-location.component';
import { FAQComponent } from './components/faq/faq.component';

import { SerchRetaurantComponent } from './components/serch-retaurant/serch-retaurant.component';
import { ShoppingCartService } from 'src/app/services/ShoppingCart.service';
import { CartPageComponent } from './components/cart-page/cart-page.component';
import { RouterModule } from '@angular/router';
import { SidebarModule } from '@syncfusion/ej2-angular-navigations';

import { StepsComponent } from './components/steps/steps.component';

import { ReservationContainerComponent } from './components/reservation-container/reservation-container.component';
import { ReservationFormComponent } from './components/reservation-form/reservation-form.component';
import { RestaurantSelectionComponent } from './components/restaurant-selection/restaurant-selection.component';

import { TableService } from './services/table.service';

import { PaymentComponent } from './components/order-details/payment/payment.component';
import { ReserveTableComponent } from './components/reserve-table/reserve-table.component';
import { SearchTableComponent } from './components/reserve-table/search-table/search-table.component';
import { TableReservationComponent } from './components/restaurant/table-reservation/table-reservation.component';
import { BookContainerComponent } from './components/restaurant/book-container/book-container.component';
import { BookSelectionComponent } from './components/restaurant/book-selection/book-selection.component';
import { PalstineComponent } from './components/palstine/palstine.component';
import { RecipeComponent } from './components/recipe/recipe.component';

import { CreateResturantComponent } from './components/create-resturant/create-resturant.component';
import { CreateRecipeComponent } from './components/create-recipe/create-recipe.component';

import { FeedbackReviewsComponent } from './components/feedback-reviews/feedback-reviews.component';

import { StatusComponent } from './components/status/status.component';
import { OrdersComponent } from './components/orders/orders.component';
import { OrderUserDetailsComponent } from './components/order-user-details/order-user-details.component';

import { AdminOrdersComponent } from './components/admin-orders/admin-orders.component';
import { AdminOrderDetailsComponent } from './components/admin-order-details/admin-order-details.component';
import { AdminOrderContainerComponent } from './components/admin-order-container/admin-order-container.component';
import { AdminTablesComponent } from './components/admin-tables/admin-tables.component';
import { ProfileComponent } from './components/profile/profile.component';

import { FeedbackScoreComponent } from './components/feedback-score/feedback-score.component';
import { RecipeFeedbackComponent } from './components/recipe-feedback/recipe-feedback.component';
import { RecipeFeddbackReviewsComponent } from './components/recipe-feddback-reviews/recipe-feddback-reviews.component';
import { MapComponent } from './components/map/map.component';
import { CreateCoponComponent } from './components/create-copon/create-copon.component';
import { CreateMenuComponent } from './components/create-menu/create-menu.component';
import { CreateTableComponent } from './components/create-table/create-table.component';

import { DashBoardComponent } from './components/dash-board/dash-board.component';

import { PaginationComponent } from './pagination/pagination.component';
import { SearchReceipeResturantComponent } from './components/search-receipe-resturant/search-receipe-resturant.component';
import { SystemAdminDashboardComponent } from './components/system-admin-dashboard/system-admin-dashboard.component';
import { UpdateRecipeComponent } from './components/update-recipe/update-recipe.component';
import { UpdateRestaurantComponent } from './components/update-restaurant/update-restaurant.component';
import { MostPopularRecipesComponent } from './components/most-popular-recipes/most-popular-recipes.component';
import { ToTopButtonComponent } from './components/to-top-button/to-top-button.component';





@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    FooterComponent,
    ErrorComponent,
    RecipeDetailsComponent,
    OrderDetailsComponent,
    ShoppingCartComponent,
    LoginComponent,
    SignupComponent,
    FeedbackComponent,
    SliderComponent,
    RestaurantOrdersComponent,
    NavbarComponent,
    MostPopularComponent,
    SearchComponent,
    RestaurantComponent,
    ImagesComponent,
    InfoComponent,
    OffersComponent,
    ReservationsComponent,
    ContactComponent,
    MenuComponent,
    RestaurantOwlCarouselComponent,
    GetLocationComponent,
    FAQComponent,
    SerchRetaurantComponent,
    CartPageComponent,
    StepsComponent,
    ReservationContainerComponent,
    ReservationFormComponent,
    RestaurantSelectionComponent,
    PaymentComponent,
    ReserveTableComponent,
    SearchTableComponent,
    TableReservationComponent,
    BookContainerComponent,
    BookSelectionComponent,
    PalstineComponent,
    HeaderComponent,

    CreateResturantComponent,
    CreateRecipeComponent,
    CreateCoponComponent,
    CreateMenuComponent,
    CreateTableComponent,

    FeedbackReviewsComponent,
    RecipeComponent,
    StatusComponent,
    OrdersComponent,
    OrderUserDetailsComponent,

    AdminOrdersComponent,
    AdminOrderDetailsComponent,
    AdminOrderContainerComponent,
    AdminTablesComponent,

    ProfileComponent,

    FeedbackScoreComponent,
    RecipeFeedbackComponent,
    RecipeFeddbackReviewsComponent,
    MapComponent,
    DashBoardComponent,
    PaginationComponent,
    SearchReceipeResturantComponent,

    SystemAdminDashboardComponent,

    UpdateRecipeComponent,
    UpdateRestaurantComponent,
    MostPopularRecipesComponent,
    ToTopButtonComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    NgxStarRatingModule,
    FormsModule,
    CarouselModule,
    RouterModule,
    SidebarModule,
    ReactiveFormsModule,
    SidebarModule,
    CommonModule,
    HammerModule,
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent],
})
export class AppModule {}
