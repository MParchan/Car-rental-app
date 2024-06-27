import React, { useState } from "react";
import { Car } from "../../../../types/models/car.types";
import Input from "../../../ui/input/Input";
import { Location } from "../../../../types/models/location.types";
import Button from "../../../ui/button/Button";
import { differenceInDays, parseISO } from "date-fns";
import { Reservation } from "../../../../types/models/reservation.types";
import { createReservation } from "../../../../api/services/reservationService";
import axios from "axios";
import { useNavigate } from "react-router-dom";

interface RentCarModalProps {
  car: Car;
  rentalLocationId: number;
  startDate: string;
  endDate: string;
  locations: Location[];
}
export default function RentCarModal({
  car,
  rentalLocationId,
  startDate,
  endDate,
  locations
}: RentCarModalProps) {
  const [returnLocationId, setReturnLocation] = useState(0);
  const [loader, setLoader] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();

  const rentalDays = differenceInDays(parseISO(endDate), parseISO(startDate)) + 1;
  let rentalPrice = Number((rentalDays * car.pricePerDay).toFixed(2));
  if (rentalDays >= 5) {
    rentalPrice = Number((rentalPrice * 0.8).toFixed(2));
  } else if (rentalDays >= 3) {
    rentalPrice = Number((rentalPrice * 0.9).toFixed(2));
  }

  const handleReserveCar = async () => {
    try {
      setLoader(true);
      if (car.carId && startDate && endDate && rentalLocationId && returnLocationId) {
        const location: Reservation = {
          reservationId: 0,
          carId: car.carId,
          userEmail: "",
          rentalLocationId: rentalLocationId,
          returnLocationId: returnLocationId,
          startDate: startDate,
          endDate: endDate,
          rentPrice: rentalPrice,
          status: "Pending"
        };
        await createReservation(location);
        navigate("/");
      }

      setLoader(false);
    } catch (error) {
      if (axios.isAxiosError(error)) {
        setError(error.response?.data.errorMessage);
      } else {
        setError("Error checking available cars");
      }
      setLoader(false);
    }
  };

  return (
    <div className="bg-white py-4 px-8 sm:px-20 rounded-lg">
      <div className="flex justify-center">
        <h2 className="text-lg font-semibold py-2">Make a reservation</h2>
      </div>
      <div className="py-2">
        <div className="flex justify-center">
          <p className="text-xl">
            {car.model?.brand?.name} {car.model?.name}
          </p>
        </div>
        <div className="flex justify-center ">
          <p className="text-xl">{car.version}</p>
        </div>
      </div>
      <div className="py-2">
        <div className="flex justify-center">
          <label className="text-xl" htmlFor="startDateInput">
            Start date
          </label>
        </div>
        <div className="flex justify-center">
          <Input type="date" name="startDate" value={startDate} disabled={true} />
        </div>
      </div>
      <div className="py-2">
        <div className="flex justify-center">
          <label className="text-xl" htmlFor="startDateInput">
            End date
          </label>
        </div>
        <div className="flex justify-center">
          <Input type="date" name="endDate" value={endDate} disabled={true} />
        </div>
      </div>
      <div className="py-2">
        <div className="flex justify-center">
          <label className="text-xl" htmlFor="startDateInput">
            Rental location
          </label>
        </div>
        <div className="flex justify-center">
          <Input
            type="string"
            name="rentaLocation"
            value={locations.find((loc) => loc.locationId === rentalLocationId)?.name}
            disabled={true}
          />
        </div>
      </div>
      <div className="py-2">
        <div className="flex justify-center">
          <label className="text-xl" htmlFor="startDateInput">
            Return location
          </label>
        </div>
        <div className="flex justify-center">
          <select
            id="locationSelect"
            name="locationSelect"
            value={returnLocationId}
            onChange={(e) => setReturnLocation(Number(e.target.value))}
            className="w-full p-2 rounded-lg bg-gray-50 border border-gray-300 text-gray-900 focus:border-gray-900 focus:border-2 hover:cursor-pointer"
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
      <div className="py-2 text-xl">
        <div className="flex justify-center">
          <p>Rental price</p>
        </div>
        <div className="flex justify-center ">
          {!rentalDays || rentalDays < 3 ? (
            <p>{rentalPrice}€</p>
          ) : rentalDays < 5 ? (
            <div className=" flex items-end">
              <p className="line-through">{(car.pricePerDay * rentalDays).toFixed(2)}€</p>
              <div className="flex bg-gray-400 ml-1 mr-2 mb-1 text-xs text-white rounded items-center px-1 h-5">
                -10%
              </div>
              <p>{rentalPrice}€</p>
            </div>
          ) : (
            <div className=" flex items-end">
              <p className="line-through">{(car.pricePerDay * rentalDays).toFixed(2)}€</p>
              <div className="flex bg-gray-400 ml-1 mr-2 mb-1 text-xs text-white rounded items-center px-1 h-5">
                -20%
              </div>
              <p>{rentalPrice}€</p>
            </div>
          )}
        </div>
      </div>
      <div className="py-2">
        {error && <div className="text-red-500 text-center">{error}</div>}
        <Button
          isDisabled={
            !car.carId || !startDate || !endDate || !rentalLocationId || !returnLocationId
          }
          onClick={handleReserveCar}
          isLoading={loader}
        >
          Reserve
        </Button>
      </div>
    </div>
  );
}
