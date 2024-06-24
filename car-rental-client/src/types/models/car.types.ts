import { Model } from "./model.types";

export type Car = {
    barId: number;
    modelId: number;
    version: string;
    pricePerDay: number;
    productionYear: number;
    horsepower: number;
    range: number;
    model?: Model;
};
