import React, { useEffect, useState } from "react";
import ModelFilters from "../components/models/modelFilters/ModelFilters";
import ModelList from "../components/models/modeList/ModelList";
import { getModels } from "../../api/services/modelService";
import { Model } from "../../types/models/model.types";
import Loader from "../ui/loader/Loader";
import { CarType } from "../../types/models/carType.types";
import { getCarTypes } from "../../api/services/carTypeService";

export default function MainPage() {
  const [models, setModels] = useState<Model[]>([]);
  const [filteredModels, setFilteredModels] = useState<Model[]>([]);
  const [carTypes, setCarTypes] = useState<CarType[]>([]);
  const [loading, setLoading] = useState(true);
  const [selectedCarTypes, setSelectedCarTypes] = useState<number[]>([]);
  const [minSeats, setMinSeats] = useState<number | null>(null);
  const [maxSeats, setMaxSeats] = useState<number | null>(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const fetchedModels = await getModels();
        const fetchedCarTypes = await getCarTypes();
        setModels(fetchedModels);
        setFilteredModels(fetchedModels);
        setCarTypes(fetchedCarTypes);
        setLoading(false);
      } catch (error) {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  useEffect(() => {
    const filterModels = () => {
      let filtered = models;

      if (selectedCarTypes.length > 0) {
        filtered = filtered.filter((model) => selectedCarTypes.includes(model.carTypeId));
      }

      if (minSeats !== null) {
        filtered = filtered.filter((model) => model.seatsNumber >= minSeats);
      }

      if (maxSeats !== null) {
        filtered = filtered.filter((model) => model.seatsNumber <= maxSeats);
      }

      setFilteredModels(filtered);
    };

    filterModels();
  }, [selectedCarTypes, models, minSeats, maxSeats]);

  const handleSelectedCarTypesChange = (selectedTypes: number[]) => {
    setSelectedCarTypes(selectedTypes);
  };

  const handleMinSeatsChange = (minSeats: number | null) => {
    setMinSeats(minSeats);
  };

  const handleMaxSeatsChange = (maxSeats: number | null) => {
    setMaxSeats(maxSeats);
  };

  if (loading) {
    return <Loader />;
  }
  return (
    <div className="md:flex">
      <ModelFilters
        carTypes={carTypes}
        onSelectedCarTypesChange={handleSelectedCarTypesChange}
        onMinSeatsChange={handleMinSeatsChange}
        onMaxSeatsChange={handleMaxSeatsChange}
      />
      <ModelList models={filteredModels} />
    </div>
  );
}
