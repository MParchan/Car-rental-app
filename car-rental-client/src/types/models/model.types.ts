import { Brand } from "./brand.types";
import { CarType } from "./carType.types";

export type Model = {
    ModelId: number;
    BrandId: number;
    CarTypeId: number;
    Name: string;
    SeatsNumber: number;
    ImageUrl: string;
    Brand?: Brand;
    CarType?: CarType;
};
