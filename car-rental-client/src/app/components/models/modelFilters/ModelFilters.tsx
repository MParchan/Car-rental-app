import React, { useState } from "react";
import { CarType } from "../../../../types/models/carType.types";
import Input from "../../../ui/input/Input";

interface ModelFiltersProps {
  carTypes: CarType[];
  onSelectedCarTypesChange: (selectedTypes: number[]) => void;
  onMinSeatsChange: (minSeats: number | null) => void;
  onMaxSeatsChange: (maxSeats: number | null) => void;
}

export default function ModelFilters({
  carTypes,
  onSelectedCarTypesChange,
  onMinSeatsChange,
  onMaxSeatsChange
}: ModelFiltersProps) {
  const [selectedCarTypes, setSelectedCarTypes] = useState<number[]>([]);
  const [isFilterVisible, setIsFilterVisible] = useState(false);

  const toggleFilterVisibility = () => {
    setIsFilterVisible(!isFilterVisible);
  };

  const handleCheckboxChange = (carTypeId: number) => {
    const newSelectedCarTypes = selectedCarTypes.includes(carTypeId)
      ? selectedCarTypes.filter((itemId) => itemId !== carTypeId)
      : [...selectedCarTypes, carTypeId];
    setSelectedCarTypes(newSelectedCarTypes);
    onSelectedCarTypesChange(newSelectedCarTypes);
  };

  const handleMinSeatsChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value ? parseInt(e.target.value) : null;
    onMinSeatsChange(value);
  };

  const handleMaxSeatsChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value ? parseInt(e.target.value) : null;
    onMaxSeatsChange(value);
  };

  return (
    <div className="h-full px-3 md:px-0 mb-2">
      <button
        className="md:hidden p-2 my-2 bg-blue-500 text-white rounded"
        onClick={toggleFilterVisibility}
      >
        Filters
      </button>
      <div
        className={`border rounded-lg px-3 w-64 md:block h-full ${
          isFilterVisible ? "block" : "hidden"
        }`}
      >
        <div className="my-4">
          <p className="text-xl py-2">Body type</p>
          {carTypes.map((carType) => (
            <div key={carType.carTypeId} className="flex">
              <input
                type="checkbox"
                id={`checkbox-${carType.carTypeId}`}
                name={`checkbox-${carType.carTypeId}`}
                checked={selectedCarTypes.includes(carType.carTypeId)}
                onChange={() => handleCheckboxChange(carType.carTypeId)}
              />
              <label htmlFor={`checkbox-${carType.carTypeId}`} className="ml-2 text-lg">
                {carType.name}
              </label>
            </div>
          ))}
        </div>
        <div className="my-8">
          <p className="text-xl py-2">Number of seats:</p>
          <div className="flex">
            <div className="w-1/2 mr-12">
              <label htmlFor="maxSeats" className="ml-2 text-lg">
                Min
              </label>
              <Input type="number" onChange={handleMinSeatsChange} />
            </div>
            <div className="w-1/2 mr-12">
              <label htmlFor="minSeats" className="ml-2 text-lg">
                Max
              </label>
              <Input type="number" name="maxSeats" onChange={handleMaxSeatsChange} />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
