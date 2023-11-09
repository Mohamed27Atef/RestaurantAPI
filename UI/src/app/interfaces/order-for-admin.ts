import { OrderItems } from "./order-items";

export interface OrderForAdmin {
    orderId:number;
    customerName:string;
    country:string;
    street:string;
    city:string;
    date:string;
    status:string;
    totalPrice:number;
    customerPhone:string;
    formattedDateTime:string | null,
}
