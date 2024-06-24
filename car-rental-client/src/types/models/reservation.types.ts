import { Car } from "./Car.types";
import { Location } from "./location.types";

export type Reservation = {
    ReservationId: number;
    CarId: number;
    UserEmail: string;
    RentalLocationId: number;
    ReturnLocationId: number;
    StartDate: Date;
    EndDate: Date;
    RentPrice: number;
    Status: "Pending" | "Started" | "Cancelled" | "Completed";
    Car?: Car;
    RentalLocation?: Location;
    ReturnLocation?: Location;
};
