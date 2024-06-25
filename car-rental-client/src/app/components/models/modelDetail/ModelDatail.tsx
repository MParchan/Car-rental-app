import React from "react";
import { Model } from "../../../../types/models/model.types";

export default function ModelDatail({ model }: { model: Model }) {
  return (
    <div>
      <div className="border rounded-lg p-2 w-64"> {model.name}</div>
    </div>
  );
}
