import React, { useEffect, useState } from "react";
import { format } from "date-fns";
import { toZonedTime } from "date-fns-tz";
import { Location } from "../../../../types/models/location.types";
import { getLocations } from "../../../../api/services/locationService";
import { Model } from "../../../../types/models/model.types";
import { getAvailableCars } from "../../../../api/services/carService";
import { Car } from "../../../../types/models/car.types";

const timeZone = "Europe/Warsaw"; // GMT+2
const today = format(toZonedTime(new Date(), timeZone), "yyyy-MM-dd");
interface CarWithQuantity extends Car {
  quantity: number;
}

interface QuantityItem {
  carId: number;
  quantity: number;
}

export default function AvailableCars({ model }: { model: Model }) {
  const [locations, setLocations] = useState<Location[]>([]);
  const [selectedLocation, setSelectedLocation] = useState(0);
  const [loader, setLoader] = useState(false);
  const [startDateValue, setStartDateValue] = useState<string>("");
  const [endDateValue, setEndDateValue] = useState<string>("");
  const [carsWithQuantities, setCarsWithQuantities] = useState<CarWithQuantity[] | undefined>(
    undefined
  );

  useEffect(() => {
    const fetchLocations = async () => {
      try {
        const fetchedLocations = await getLocations();
        setLocations(fetchedLocations);
      } catch (error) {
        console.error("Error fetching models:", error);
      }
    };
    fetchLocations();
  }, []);

  const handleStartDateChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setStartDateValue(e.target.value);
    if (!e.target.value) {
      setEndDateValue(e.target.value);
    }
  };
  const handleEndDateChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setEndDateValue(e.target.value);
  };

  const handleCheckAvailableCars = async () => {
    try {
      setLoader(true);
      if (selectedLocation && startDateValue && endDateValue) {
        const fetchedCars = await getAvailableCars(
          model.modelId,
          selectedLocation,
          startDateValue,
          endDateValue
        );
        let cars = model.cars?.map((car) => {
          const quantityData = fetchedCars.find((item: QuantityItem) => item.carId === car.carId);
          return {
            ...car,
            quantity: quantityData ? quantityData.quantity : 0
          };
        });
        if (cars) {
          cars = cars
            .filter((car) => car.quantity > 0)
            .sort((a, b) => a.pricePerDay - b.pricePerDay);
          setCarsWithQuantities(cars);
        }
      }
      setLoader(false);
    } catch (error) {
      console.error("Error fetching cars:", error);
      setLoader(false);
    }
  };

  return (
    <div className="mt-12 ">
      <p className="text-3xl fond-semibold mb-2">Check available cars</p>
      <div className="border rounded-xl p-4">
        <div className="md:flex justify-between mb-6 px-2 xl:px-8">
          <div>
            <div className="flex justify-center mb-2">
              <label className="text-xl" htmlFor="locationSelect">
                Rental location
              </label>
            </div>
            <div className="flex justify-center mb-2">
              <select
                id="locationSelect"
                name="locationSelect"
                value={selectedLocation}
                onChange={(e) => setSelectedLocation(Number(e.target.value))}
                className="w-60 md:w-[200px] lg:w-64 p-2 rounded-lg bg-gray-50 border border-gray-300 text-gray-900 focus:border-gray-900 focus:border-2"
              >
                <option value={0}>Choose rental location</option>
                {locations.map((location) => (
                  <option key={location.locationId} value={location.locationId} className="p-2">
                    {location.name}
                  </option>
                ))}
              </select>
            </div>
          </div>
          <div>
            <div className="flex justify-center mb-2">
              <label className="text-xl" htmlFor="startDateInput">
                Start date
              </label>
            </div>
            <div className="flex justify-center  mb-2">
              <input
                type="date"
                id="dateInput"
                name="startDateInput"
                min={today}
                onChange={handleStartDateChange}
                className="w-60 md:w-[200px] lg:w-64 p-2 rounded-lg bg-gray-50 border border-gray-300 text-gray-900"
              />
            </div>
          </div>
          <div>
            <div className="flex justify-center mb-2">
              <label className="text-xl" htmlFor="endDateInput">
                End date
              </label>
            </div>
            <div className="flex justify-center mb-2">
              <input
                type="date"
                id="dateInput"
                name="endDateInput"
                value={endDateValue}
                disabled={!startDateValue}
                min={startDateValue || today}
                onChange={handleEndDateChange}
                className={`w-60 md:w-[200px] lg:w-64 p-2 rounded-lg bg-gray-50 border border-gray-300 
                    ${!startDateValue ? "text-gray-400" : "text-gray-900"}`}
              />
            </div>
          </div>
        </div>
        <div className="flex justify-center ">
          <button
            className={`bg-blue-500 text-white font-medium py-2 w-48 rounded-lg 
                ${!selectedLocation || !startDateValue || !endDateValue ? "hover:cursor-not-allowed" : "hover:bg-blue-400"}`}
            onClick={handleCheckAvailableCars}
            disabled={!selectedLocation || !startDateValue || !endDateValue}
          >
            {loader ? (
              <svg
                aria-hidden="true"
                role="status"
                className="inline w-6 h-6 text-white animate-spin"
                viewBox="0 0 100 101"
                fill="none"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  d="M100 50.5908C100 78.2051 77.6142 100.591 50 100.591C22.3858 100.591 0 78.2051 0 50.5908C0 22.9766 22.3858 0.59082 50 0.59082C77.6142 0.59082 100 22.9766 100 50.5908ZM9.08144 50.5908C9.08144 73.1895 27.4013 91.5094 50 91.5094C72.5987 91.5094 90.9186 73.1895 90.9186 50.5908C90.9186 27.9921 72.5987 9.67226 50 9.67226C27.4013 9.67226 9.08144 27.9921 9.08144 50.5908Z"
                  fill="#E5E7EB"
                />
                <path
                  d="M93.9676 39.0409C96.393 38.4038 97.8624 35.9116 97.0079 33.5539C95.2932 28.8227 92.871 24.3692 89.8167 20.348C85.8452 15.1192 80.8826 10.7238 75.2124 7.41289C69.5422 4.10194 63.2754 1.94025 56.7698 1.05124C51.7666 0.367541 46.6976 0.446843 41.7345 1.27873C39.2613 1.69328 37.813 4.19778 38.4501 6.62326C39.0873 9.04874 41.5694 10.4717 44.0505 10.1071C47.8511 9.54855 51.7191 9.52689 55.5402 10.0491C60.8642 10.7766 65.9928 12.5457 70.6331 15.2552C75.2735 17.9648 79.3347 21.5619 82.5849 25.841C84.9175 28.9121 86.7997 32.2913 88.1811 35.8758C89.083 38.2158 91.5421 39.6781 93.9676 39.0409Z"
                  fill="currentColor"
                />
              </svg>
            ) : (
              "Check"
            )}
          </button>
        </div>
      </div>

      {carsWithQuantities ? (
        <div className="mt-8">
          <p className="text-3xl fond-semibold my-4">
            {carsWithQuantities.length > 0 ? "Cars:" : "No cars available"}
          </p>
          <ul>
            {carsWithQuantities.map((car) => (
              <li key={car.carId} className={`${car.quantity > 0 ? "block" : "hidden"}`}>
                <div className="border sm:flex justify-between rounded-lg my-4 py-2 px-4">
                  <div className="">
                    <div className="font-semibold text-2xl mb-2">{car.version}</div>
                    <div>
                      <p className="text-lg">
                        <span className="font-semibold">Year of production:</span>{" "}
                        {car.productionYear}
                      </p>
                      <p className="text-lg">
                        <span className="font-semibold">Power:</span> {car.horsepower} KM
                      </p>
                      <p className="text-lg">
                        <span className="font-semibold">Range:</span> {car.range} km
                      </p>
                      <p className="text-lg">
                        <span className="font-semibold">Price per day:</span> {car.pricePerDay}€
                      </p>
                    </div>
                  </div>
                  <div className="flex items-center justify-center my-2">
                    <button className="bg-blue-500 text-white font-medium py-2 w-48 rounded-lg hover:bg-blue-400">
                      Rent
                    </button>
                  </div>
                </div>
              </li>
            ))}
          </ul>
        </div>
      ) : null}
    </div>
  );
}
