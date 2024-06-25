import { Brand } from "./brand.types";
import { Car } from "./car.types";
import { CarType } from "./carType.types";

export type Model = {
    modelId: number;
    brandId: number;
    carTypeId: number;
    name: string;
    seatsNumber: number;
    imageUrl: string;
    brand?: Brand;
    carType?: CarType;
    cars?: Car[];
};
