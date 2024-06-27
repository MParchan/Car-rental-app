import React, { useEffect, useState } from "react";
import ModelFilters from "../components/models/modelFilters/ModelFilters";
import ModelList from "../components/models/modeList/ModelList";
import { getModels } from "../../api/services/modelService";
import { Model } from "../../types/models/model.types";
import Loader from "../ui/loader/Loader";

export default function MainPage() {
  const [models, setModels] = useState<Model[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchModels = async () => {
      try {
        const fetchedModels = await getModels();
        setModels(fetchedModels);
        setLoading(false);
      } catch (error) {
        setLoading(false);
      }
    };

    fetchModels();
  }, []);

  if (loading) {
    return <Loader />;
  }
  return (
    <div className="flex">
      <ModelFilters />
      <ModelList models={models} />
    </div>
  );
}
