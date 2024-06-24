import { Car } from "./car.types";
import { Location } from "./location.types";

export type LocationCar = {
    locationCarId: number;
    locationId: number;
    carId: number;
    quantity: number;
    location?: Location;
    car?: Car;
};
