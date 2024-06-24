import React from "react";
import ModelFilters from "../components/models/modelFilters/ModelFilters";
import ModelList from "../components/models/modeList/ModelList";

export default function MainPage() {
  return (
    <div className="flex">
      <ModelFilters />
      <ModelList />
    </div>
  );
}
