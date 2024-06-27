import React from "react";
import { Model } from "../../../../types/models/model.types";
import ModelItem from "../modelItem/ModelItem";

export default function ModelList({ models }: { models: Model[] }) {
  return (
    <div className="flex-1 pl-4">
      <div className="">
        <span className="text-4xl">List of models:</span>
      </div>
      <div>
        {models.length === 0 && (
          <div className="flex justify-center mt-12">
            <p className="text-3xl">No models available</p>
          </div>
        )}
        {models.map((model) => (
          <ModelItem key={model.modelId} model={model} />
        ))}
      </div>
    </div>
  );
}
