import React, { useEffect, useState } from "react";
import { getModels } from "../../../../api/services/modelService";
import { Model } from "../../../../types/models/model.types";
import Loader from "../../loader/Loader";
import ModelItem from "../modelItem/ModelItem";

export default function ModelList() {
  const [models, setModels] = useState<Model[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchModels = async () => {
      try {
        const fetchedModels = await getModels();
        setModels(fetchedModels);
        setLoading(false);
      } catch (error) {
        console.error("Error fetching models:", error);
        setLoading(false);
      }
    };

    fetchModels();
  }, []);

  return (
    <div className="flex-1 pl-4">
      <div className="">
        <span className="text-4xl font-semibold">List of models:</span>
      </div>
      {loading ? (
        <Loader />
      ) : (
        <div>
          {models.map((model) => (
            <ModelItem key={model.modelId} model={model} />
          ))}
        </div>
      )}
    </div>
  );
}
