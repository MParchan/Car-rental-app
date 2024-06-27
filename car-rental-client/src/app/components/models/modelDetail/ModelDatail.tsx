import React from "react";
import { Model } from "../../../../types/models/model.types";
import AvailableCars from "../../cars/availableCars/AvailableCars";

export default function ModelDatail({ model }: { model: Model }) {
  return (
    <div>
      <div className="lg:flex">
        <div className="flex-1">
          <div className="text-3xl my-6">
            {model.brand?.name} {model.name}
          </div>
          <div className="hidden lg:block">
            <div className="text-xl my-2">Body type: {model.carType?.name}</div>
            <div className="text-xl my-2">Number of seats: {model.seatsNumber}</div>
          </div>
        </div>
        <div className="justify-center lg:w-[700px]">
          <img
            src={model.imageUrl}
            className="rounded-lg"
            alt={`${model.brand?.name} ${model.name} Logo`}
          />
        </div>
        <div className="block lg:hidden">
          <div className="text-xl my-2">Body type: {model.carType?.name}</div>
          <div className="text-xl my-2">Number of seats: {model.seatsNumber}</div>
        </div>
      </div>
      <AvailableCars model={model} />
    </div>
  );
}
