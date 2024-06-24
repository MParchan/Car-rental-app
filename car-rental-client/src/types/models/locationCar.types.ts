import { Car } from "./Car.types";
import { Location } from "./location.types";

export type LocationCar = {
    LocationCarId: number;
    LocationId: number;
    CarId: number;
    Quantity: number;
    Location?: Location;
    Car?: Car;
};
