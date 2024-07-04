import React, { useEffect, useRef, useState } from "react";
import { Reservation } from "../../../../types/models/reservation.types";
import {
  cancelReservation,
  endReservation,
  startReservation
} from "../../../../api/services/reservationService";
import { format, parseISO } from "date-fns";
import AdminReservationsTableRow from "./AdminReservationsTableRow";

interface AdminReservationsTableProps {
  reservations: Reservation[];
  onFetchReservations: () => Promise<void>;
  sortField: string;
  setSortField: (sortField: string) => void;
  sortOrder: string;
  setSortOrder: (sortOrder: string) => void;
}

export default function AdminReservationsTable({
  reservations,
  onFetchReservations,
  sortField,
  setSortField,
  sortOrder,
  setSortOrder
}: AdminReservationsTableProps) {
  const [selectedReservationId, setSelectedReservationId] = useState<number | null>(null);
  const [isCancelModalOpen, setIsCancelModalOpen] = useState(false);
  const [isStartModalOpen, setIsStartModalOpen] = useState(false);
  const [isEndModalOpen, setIsEndModalOpen] = useState(false);
  const cancelModalRef = useRef<HTMLDivElement>(null);
  const startModalRef = useRef<HTMLDivElement>(null);
  const endModalRef = useRef<HTMLDivElement>(null);

  const handleShowCancelModal = (reservationId: number) => {
    setSelectedReservationId(reservationId);
    setIsCancelModalOpen(true);
  };

  const handleShowStarModal = (reservationId: number) => {
    setSelectedReservationId(reservationId);
    setIsStartModalOpen(true);
  };

  const handleShowEndModal = (reservationId: number) => {
    setSelectedReservationId(reservationId);
    setIsEndModalOpen(true);
  };

  const selectedReservation = reservations.find(
    (reservation) => reservation.reservationId === selectedReservationId
  );

  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (cancelModalRef.current && !cancelModalRef.current.contains(event.target as Node)) {
        setIsCancelModalOpen(false);
      }
      if (startModalRef.current && !startModalRef.current.contains(event.target as Node)) {
        setIsStartModalOpen(false);
      }
      if (endModalRef.current && !endModalRef.current.contains(event.target as Node)) {
        setIsEndModalOpen(false);
      }
    };
    document.addEventListener("mousedown", handleClickOutside);
    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, []);

  const handleCancelReservation = async () => {
    if (selectedReservationId) {
      await cancelReservation(selectedReservationId);
    }
    setIsCancelModalOpen(false);
    onFetchReservations();
  };

  const handleStartReservation = async () => {
    if (selectedReservationId) {
      await startReservation(selectedReservationId);
    }
    setIsStartModalOpen(false);
    onFetchReservations();
  };

  const handleEndReservation = async () => {
    if (selectedReservationId) {
      await endReservation(selectedReservationId);
    }
    setIsEndModalOpen(false);
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
              onClick={() => handleSort("UserEmail")}
            >
              User {sortField === "UserEmail" ? (sortOrder === "asc" ? "▲" : "▼") : ""}
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
            <AdminReservationsTableRow
              onShowCancelModal={handleShowCancelModal}
              onShowStartModal={handleShowStarModal}
              onShowEndModal={handleShowEndModal}
              reservation={reservation}
              key={reservation.reservationId}
            />
          ))}
        </tbody>
      </table>
      {isCancelModalOpen && selectedReservation && (
        <div className="fixed top-0 left-0 w-full h-full bg-black bg-opacity-50 flex justify-center items-center z-50">
          <div className="bg-white py-4 px-16 sm:px-20 rounded-lg" ref={cancelModalRef}>
            <h2 className="text-lg font-semibold mb-4">Cancel reservation</h2>
            <p className="text-lg">
              Car: {selectedReservation.car?.model?.brand?.name}{" "}
              {selectedReservation.car?.model?.name}
            </p>
            <p className="text-lg">User: {selectedReservation.userEmail}</p>
            <p className="text-lg">
              Start date: {format(parseISO(selectedReservation.startDate.toString()), "dd.MM.yyyy")}
            </p>
            <p className="text-lg">
              End date: {format(parseISO(selectedReservation.endDate.toString()), "dd.MM.yyyy")}
            </p>
            <p className="text-lg">Rental location: {selectedReservation.rentalLocation?.name}</p>
            <p className="text-lg">Return location: {selectedReservation.returnLocation?.name}</p>
            <button
              className="bg-red-500 text-white px-4 py-2 mt-4 rounded-lg"
              onClick={handleCancelReservation}
            >
              Cancel reservation
            </button>
          </div>
        </div>
      )}
      {isStartModalOpen && selectedReservation && (
        <div className="fixed top-0 left-0 w-full h-full bg-black bg-opacity-50 flex justify-center items-center z-50">
          <div className="bg-white py-4 px-16 sm:px-20 rounded-lg" ref={startModalRef}>
            <h2 className="text-lg font-semibold mb-4">Start reservation</h2>
            <p className="text-lg">
              Car: {selectedReservation.car?.model?.brand?.name}{" "}
              {selectedReservation.car?.model?.name}
            </p>
            <p className="text-lg">User: {selectedReservation.userEmail}</p>
            <p className="text-lg">
              Start date: {format(parseISO(selectedReservation.startDate.toString()), "dd.MM.yyyy")}
            </p>
            <p className="text-lg">
              End date: {format(parseISO(selectedReservation.endDate.toString()), "dd.MM.yyyy")}
            </p>
            <p className="text-lg">Rental location: {selectedReservation.rentalLocation?.name}</p>
            <p className="text-lg">Return location: {selectedReservation.returnLocation?.name}</p>
            <button
              className="bg-green-500 text-white px-4 py-2 mt-4 rounded-lg"
              onClick={handleStartReservation}
            >
              Start reservation
            </button>
          </div>
        </div>
      )}
      {isEndModalOpen && selectedReservation && (
        <div className="fixed top-0 left-0 w-full h-full bg-black bg-opacity-50 flex justify-center items-center z-50">
          <div className="bg-white py-4 px-16 sm:px-20 rounded-lg" ref={endModalRef}>
            <h2 className="text-lg font-semibold mb-4">End reservation</h2>
            <p className="text-lg">
              Car: {selectedReservation.car?.model?.brand?.name}{" "}
              {selectedReservation.car?.model?.name}
            </p>
            <p className="text-lg">User: {selectedReservation.userEmail}</p>
            <p className="text-lg">
              Start date: {format(parseISO(selectedReservation.startDate.toString()), "dd.MM.yyyy")}
            </p>
            <p className="text-lg">
              End date: {format(parseISO(selectedReservation.endDate.toString()), "dd.MM.yyyy")}
            </p>
            <p className="text-lg">Rental location: {selectedReservation.rentalLocation?.name}</p>
            <p className="text-lg">Return location: {selectedReservation.returnLocation?.name}</p>
            <button
              className="bg-yellow-500 text-white px-4 py-2 mt-4 rounded-lg"
              onClick={handleEndReservation}
            >
              End reservation
            </button>
          </div>
        </div>
      )}
    </div>
  );
}
