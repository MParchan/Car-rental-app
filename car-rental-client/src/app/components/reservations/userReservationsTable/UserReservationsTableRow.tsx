import React from "react";
import { Reservation } from "../../../../types/models/reservation.types";
import { format, parseISO } from "date-fns";

const statusColors: { [key: string]: string } = {
  Pending: "text-blue-500",
  Started: "text-green-500",
  Cancelled: "text-red-500",
  Completed: "text-yellow-500"
};

interface UserReservationsTableRowProps {
  reservation: Reservation;
  onShowModal: (reservationId: number) => void;
}

export default function UserReservationsTableRow({
  reservation,
  onShowModal
}: UserReservationsTableRowProps) {
  const handleShowModal = () => {
    onShowModal(reservation.reservationId);
  };

  return (
    <tr className="odd:bg-white odd:dark:bg-gray-900 even:bg-gray-50">
      <th className="px-1 xl:px-6 py-4 font-medium text-gray-900 whitespace-nowrap">
        {reservation.car?.model?.brand?.name} {reservation.car?.model?.name}
      </th>
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
      <td className="px-1 xl:px-6 py-4">{reservation.rentPrice.toFixed(2)} €</td>
      <td className="px-1 py-4">
        {reservation.status === "Pending" && (
          <button
            className="ml-2 px-2 py-1 bg-red-500 text-white rounded-lg"
            onClick={handleShowModal}
          >
            Cancel
          </button>
        )}
      </td>
    </tr>
  );
}
