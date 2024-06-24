import { Model } from "./model.types";

export type Car = {
    CarId: number;
    ModelId: number;
    Version: string;
    PricePerDay: number;
    ProductionYear: number;
    Horsepower: number;
    Range: number;
    Model?: Model;
};
