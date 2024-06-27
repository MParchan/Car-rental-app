import React from "react";
import { Reservation } from "../../../../types/models/reservation.types";
import { format, parseISO } from "date-fns";

const statusColors: { [key: string]: string } = {
  Pending: "text-blue-500",
  Started: "text-green-500",
  Cancelled: "text-red-500",
  Completed: "text-yellow-500"
};

interface AdminReservationsTableRowProps {
  reservation: Reservation;
  onShowCancelModal: (reservationId: number) => void;
  onShowStartModal: (reservationId: number) => void;
  onShowEndModal: (reservationId: number) => void;
}

export default function AdminReservationsTableRow({
  reservation,
  onShowCancelModal,
  onShowStartModal,
  onShowEndModal
}: AdminReservationsTableRowProps) {
  const handleShowCancelModal = () => {
    onShowCancelModal(reservation.reservationId);
  };
  const handleShowStartModal = () => {
    onShowStartModal(reservation.reservationId);
  };
  const handleShowEndModal = () => {
    onShowEndModal(reservation.reservationId);
  };

  return (
    <tr className="odd:bg-white odd:dark:bg-gray-900 even:bg-gray-50">
      <th className="px-1 xl:px-6 py-4 font-medium text-gray-900 whitespace-nowrap">
        {reservation.car?.model?.brand?.name} {reservation.car?.model?.name}
      </th>
      <td className="px-1 xl:px-6 py-4">{reservation.userEmail}</td>
      <td className="px-1 xl:px-6 py-4">
        {format(parseISO(reservation.startDate.toString()), "dd.MM.yyyy")}
      </td>
      <td className="px-1 xl:px-6 py-4">
        {format(parseISO(reservation.endDate.toString()), "dd.MM.yyyy")}
      </td>
      <td className="px-1 xl:px-6 py-4">{reservation.rentalLocation?.name}</td>
      <td className="px-1 xl:px-6 py-4">{reservation.returnLocation?.name}</td>
      <td className={`px-1 xl:px-6 py-4 ${statusColors[reservation.status]}`}>
        {reservation.status}
      </td>
      <td className="px-1 xl:px-6 py-4">{reservation.rentPrice}€</td>
      <td className="px-1 py-4 flex justify-center">
        {reservation.status === "Pending" && (
          <>
            <button
              className="px-2 py-1 bg-red-500 text-white rounded-lg"
              onClick={handleShowCancelModal}
            >
              Cancel
            </button>
            <button
              className="ml-1 px-2 py-1 bg-green-500 text-white rounded-lg"
              onClick={handleShowStartModal}
            >
              Start
            </button>
          </>
        )}
        {reservation.status === "Started" && (
          <button
            className="px-2 py-1 bg-yellow-500 text-white rounded-lg"
            onClick={handleShowEndModal}
          >
            End
          </button>
        )}
      </td>
    </tr>
  );
}
