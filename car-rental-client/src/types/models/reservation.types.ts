import { Car } from "./car.types";
import { Location } from "./location.types";

export type Reservation = {
    reservationId: number;
    carId: number;
    userEmail: string;
    rentalLocationId: number;
    returnLocationId: number;
    startDate: string;
    endDate: string;
    rentPrice: number;
    status: "Pending" | "Started" | "Cancelled" | "Completed";
    car?: Car;
    rentalLocation?: Location;
    returnLocation?: Location;
};
