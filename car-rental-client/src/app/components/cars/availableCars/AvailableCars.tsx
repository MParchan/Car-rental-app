import React, { useEffect, useRef, useState } from "react";
import { differenceInDays, format, parseISO } from "date-fns";
import { toZonedTime } from "date-fns-tz";
import { Location } from "../../../../types/models/location.types";
import { getLocations } from "../../../../api/services/locationService";
import { Model } from "../../../../types/models/model.types";
import { getAvailableCars } from "../../../../api/services/carService";
import { Car } from "../../../../types/models/car.types";
import RentCarModal from "../rentCarModal/RentCarModal";
import axios from "axios";
import Button from "../../../ui/button/Button";
import { isAuthenticated } from "../../../../api/services/authService";
import { useNavigate } from "react-router-dom";

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
  const [error, setError] = useState<string | null>(null);
  const [selectedLocation, setSelectedLocation] = useState(0);
  const [loader, setLoader] = useState(false);
  const [startDateValue, setStartDateValue] = useState<string>("");
  const [endDateValue, setEndDateValue] = useState<string>("");
  const [rentLocationId, setRendLocationId] = useState(0);
  const [rentStartDate, setRentStartDate] = useState<string>("");
  const [rentEndDate, setRentEndDate] = useState<string>("");
  const [carsWithQuantities, setCarsWithQuantities] = useState<CarWithQuantity[] | undefined>(
    undefined
  );
  const [rentCarId, setRentCarId] = useState<number | null>(null);
  const [rentalDays, setRentalDays] = useState<number | null>(null);
  const modalRef = useRef<HTMLDivElement>(null);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchLocations = async () => {
      try {
        const fetchedLocations = await getLocations();
        setLocations(fetchedLocations);
      } catch (error) {
        console.log("Error fetching locations:", error);
      }
    };
    fetchLocations();
  }, []);

  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (modalRef.current && !modalRef.current.contains(event.target as Node)) {
        setRentCarId(null);
      }
    };
    document.addEventListener("mousedown", handleClickOutside);
    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
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
      setError(null);
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
            .sort((a, b) => a.pricePerDay - b.pricePerDay)
            .map((car) => {
              return { ...car, model: model };
            });
          setCarsWithQuantities(cars);
          setRendLocationId(selectedLocation);
          setRentStartDate(startDateValue);
          setRentEndDate(endDateValue);
          setRentalDays(differenceInDays(parseISO(endDateValue), parseISO(startDateValue)) + 1);
        }
        setLoader(false);
      }
    } catch (error) {
      if (axios.isAxiosError(error)) {
        setError(error.response?.data.errorMessage);
      } else {
        setError("Error checking available cars");
      }
      setCarsWithQuantities(undefined);
      setRentalDays(null);
      setLoader(false);
    }
  };

  const handleRentCar = (carId: number) => {
    if (!isAuthenticated()) {
      navigate("/sign-in");
    }
    setRentCarId(carId);
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
                className="w-60 md:w-[200px] lg:w-64 p-2 rounded-lg bg-gray-50 border border-gray-300 text-gray-900 focus:border-gray-900 focus:border-2 hover:cursor-pointer"
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
                className="w-60 md:w-[200px] lg:w-64 p-2 rounded-lg bg-gray-50 border border-gray-300 text-gray-900 hover:cursor-text"
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
                className={`w-60 md:w-[200px] lg:w-64 p-2 rounded-lg bg-gray-50 border border-gray-300 hover:cursor-text
                    ${!startDateValue ? "text-gray-400" : "text-gray-900"}`}
              />
            </div>
          </div>
        </div>
        {error && <div className="text-red-500 text-center">{error}</div>}
        <div className="flex justify-center ">
          <div className="w-48">
            <Button
              isLoading={loader}
              isDisabled={!selectedLocation || !startDateValue || !endDateValue}
              onClick={handleCheckAvailableCars}
            >
              Check
            </Button>
          </div>
        </div>
      </div>
      <div className="mt-4 text-xl">
        <div className="flex items-end">
          <p>Rental for at least 3 days</p>
          <div className="flex bg-gray-400 ml-2 mb-[2px] text-sm text-white rounded items-center px-1 h-full">
            -10%
          </div>
        </div>
        <div className="flex items-end">
          <p>Rental for at least 5 days</p>
          <div className="flex bg-gray-400 ml-2 mb-[2px] text-sm text-white rounded items-center px-1 h-full">
            -20%
          </div>
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
                        <span className="font-semibold mr-1">Year of production:</span>{" "}
                        {car.productionYear}
                      </p>
                      <p className="text-lg">
                        <span className="font-semibold mr-1">Power:</span> {car.horsepower} KM
                      </p>
                      <p className="text-lg">
                        <span className="font-semibold mr-1">Range:</span> {car.range} km
                      </p>
                      {!rentalDays || rentalDays < 3 ? (
                        <p className="text-lg">
                          <span className="font-semibold mr-1">Price per day:</span>
                          {car.pricePerDay}€
                        </p>
                      ) : rentalDays < 5 ? (
                        <div className=" flex text-lg items-end">
                          <span className="font-semibold mr-1">Price per day:</span>
                          <p className="line-through">{car.pricePerDay}€</p>
                          <div className="flex bg-gray-400 mx-1 mb-1 text-xs text-white rounded items-center px-1 h-full">
                            -10%
                          </div>
                          <p>{(car.pricePerDay * 0.9).toFixed(2)}€</p>
                        </div>
                      ) : (
                        <div className=" flex text-lg items-end">
                          <span className="font-semibold mr-1">Price per day:</span>
                          <p className="line-through">{car.pricePerDay}€</p>
                          <div className="flex bg-gray-400 mx-1 mb-1 text-xs text-white rounded items-center px-1 h-full">
                            -20%
                          </div>
                          <p>{(car.pricePerDay * 0.8).toFixed(2)}€</p>
                        </div>
                      )}
                    </div>
                  </div>
                  <div className="flex items-center justify-center my-2">
                    <div className="w-48">
                      <Button onClick={() => handleRentCar(car.carId)}>Rent</Button>
                    </div>
                  </div>
                </div>
                {rentCarId && rentCarId === car.carId && (
                  <div className="fixed top-0 left-0 w-full h-full bg-black bg-opacity-50 flex justify-center items-center z-50">
                    <div ref={modalRef}>
                      <RentCarModal
                        car={car}
                        rentalLocationId={rentLocationId}
                        startDate={rentStartDate}
                        endDate={rentEndDate}
                        locations={locations}
                      />
                    </div>
                  </div>
                )}
              </li>
            ))}
          </ul>
        </div>
      ) : null}
    </div>
  );
}
