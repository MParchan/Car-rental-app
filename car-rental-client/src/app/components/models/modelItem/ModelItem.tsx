import React from "react";
import { Model } from "../../../../types/models/model.types";
import { Link } from "react-router-dom";

export default function ModelItem({ model }: { model: Model }) {
  const urlModel = (model.brand?.name + "-" + model.name).toLowerCase().replace(/ /g, "-");

  return (
    <Link to={`/m/${urlModel}/${model.modelId}`}>
      <div className="flex my-8 p-4 border-b hover:rounded-lg hover:cursor-pointer hover:border hover:shadow-[0_10px_15px_3px_rgba(0,0,0,0.1),0_4px_6px_4px_rgba(0,0,0,0.1)]">
        <div className="flex-1">
          <div className="text-3xl fond-semibold">{model.brand?.name}</div>
          <div className="text-2xl fond-semibold mb-4">{model.name}</div>
          <div className="text-xl">Body type: {model.carType?.name}</div>
          <div className="text-xl">Number of seats: {model.seatsNumber}</div>
        </div>
        <div className="border rounded-lg w-[500px]">
          <img
            src={model.imageUrl}
            className="rounded-lg"
            alt={`${model.brand?.name} ${model.name} Logo`}
          />
        </div>
      </div>
    </Link>
  );
}
