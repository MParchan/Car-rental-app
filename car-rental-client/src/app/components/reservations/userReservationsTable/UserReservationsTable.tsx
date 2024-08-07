import React, { useEffect, useRef, useState } from "react";
import { Reservation } from "../../../../types/models/reservation.types";
import UserReservationsTableRow from "./UserReservationsTableRow";
import { format, parseISO } from "date-fns";
import { cancelReservation } from "../../../../api/services/reservationService";

interface UserReservationsTableProps {
  reservations: Reservation[];
  onFetchReservations: () => Promise<void>;
  sortField: string;
  setSortField: (sortField: string) => void;
  sortOrder: string;
  setSortOrder: (sortOrder: string) => void;
}

export default function UserReservationsTable({
  reservations,
  onFetchReservations,
  sortField,
  setSortField,
  sortOrder,
  setSortOrder
}: UserReservationsTableProps) {
  const [canceledReservationId, setCanceledReservationId] = useState<number | null>(null);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const modalRef = useRef<HTMLDivElement>(null);

  const handleShowCancelModal = (reservationId: number) => {
    setCanceledReservationId(reservationId);
    setIsModalOpen(true);
  };
  const canceledReservation = reservations.find(
    (reservation) => reservation.reservationId === canceledReservationId
  );

  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (modalRef.current && !modalRef.current.contains(event.target as Node)) {
        setIsModalOpen(false);
      }
    };
    document.addEventListener("mousedown", handleClickOutside);
    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, []);

  const handleCancelReservation = async () => {
    if (canceledReservationId) {
      await cancelReservation(canceledReservationId);
    }
    setIsModalOpen(false);
    onFetchReservations();
  };

  const handleSort = (field: string) => {
    const isAsc = sortField === field && sortOrder === "asc";
    setSortField(field);
    setSortOrder(isAsc ? "desc" : "asc");
  };

  return (
    <div className="relative overflow-x-auto shadow-md sm:rounded-lg">
      <table className="w-full text-sm text-left rtl:text-right text-gray-500">
        <thead className="text-xs text-gray-700 uppercase bg-gray-50">
          <tr>
            <th
              scope="col"
              className="px-1 xl:px-6 py-3 cursor-pointer"
              onClick={() => handleSort("CarId")}
            >
              Car {sortField === "CarId" ? (sortOrder === "asc" ? "▲" : "▼") : ""}
            </th>
            <th
              scope="col"
              className="px-1 xl:px-6 py-3 cursor-pointer"
              onClick={() => handleSort("StartDate")}
            >
              Start date {sortField === "StartDate" ? (sortOrder === "asc" ? "▲" : "▼") : ""}
            </th>
            <th
              scope="col"
              className="px-1 xl:px-6 py-3 cursor-pointer"
              onClick={() => handleSort("EndDate")}
            >
              End Date {sortField === "EndDate" ? (sortOrder === "asc" ? "▲" : "▼") : ""}
            </th>
            <th
              scope="col"
              className="px-1 xl:px-6 py-3 cursor-pointer"
              onClick={() => handleSort("RentalLocationId")}
            >
              Rental Location{" "}
              {sortField === "RentalLocationId" ? (sortOrder === "asc" ? "▲" : "▼") : ""}
            </th>
            <th
              scope="col"
              className="px-1 xl:px-6 py-3 cursor-pointer"
              onClick={() => handleSort("ReturnLocationId")}
            >
              Return Location{" "}
              {sortField === "ReturnLocationId" ? (sortOrder === "asc" ? "▲" : "▼") : ""}
            </th>
            <th
              scope="col"
              className="px-1 xl:px-6 py-3 cursor-pointer"
              onClick={() => handleSort("Status")}
            >
              Status {sortField === "Status" ? (sortOrder === "asc" ? "▲" : "▼") : ""}
            </th>
            <th
              scope="col"
              className="px-1 xl:px-6 py-3 cursor-pointer"
              onClick={() => handleSort("RentPrice")}
            >
              Price {sortField === "RentPrice" ? (sortOrder === "asc" ? "▲" : "▼") : ""}
            </th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {reservations.map((reservation) => (
            <UserReservationsTableRow
              onShowModal={handleShowCancelModal}
              reservation={reservation}
              key={reservation.reservationId}
            />
          ))}
        </tbody>
      </table>
      {isModalOpen && canceledReservation && (
        <div className="fixed top-0 left-0 w-full h-full bg-black bg-opacity-50 flex justify-center items-center z-50">
          <div className="bg-white py-4 px-16 sm:px-20 rounded-lg" ref={modalRef}>
            <h2 className="text-lg font-semibold mb-4">Cancel reservation</h2>
            <p className="text-lg">
              Car: {canceledReservation.car?.model?.brand?.name}{" "}
              {canceledReservation.car?.model?.name}
            </p>
            <p className="text-lg">
              Start date: {format(parseISO(canceledReservation.startDate.toString()), "dd.MM.yyyy")}
            </p>
            <p className="text-lg">
              End date: {format(parseISO(canceledReservation.endDate.toString()), "dd.MM.yyyy")}
            </p>
            <p className="text-lg">Rental location: {canceledReservation.rentalLocation?.name}</p>
            <p className="text-lg">Return location: {canceledReservation.returnLocation?.name}</p>
            <button
              className="bg-red-500 text-white px-4 py-2 mt-4 rounded-lg"
              onClick={handleCancelReservation}
            >
              Cancel reservation
            </button>
          </div>
        </div>
      )}
    </div>
  );
}
