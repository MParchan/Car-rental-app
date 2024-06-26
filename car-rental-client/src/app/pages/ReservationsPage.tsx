import React, { useEffect, useState } from "react";
import Loader from "../components/loader/Loader";
import { Reservation } from "../../types/models/reservation.types";
import { getReservations } from "../../api/services/reservationService";
import UserReservationsTable from "../components/reservations/userReservationsTable/UserReservationsTable";

export default function ReservationsPage() {
  const [reservations, setReservations] = useState<Reservation[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    handleFetchReservations();
  }, []);

  const handleFetchReservations = async () => {
    try {
      const fetchedReservations = await getReservations();
      setReservations(fetchedReservations);
      setLoading(false);
    } catch (error) {
      setLoading(false);
    }
  };

  if (loading) {
    return <Loader />;
  }

  return (
    <div>
      <UserReservationsTable
        reservations={reservations}
        onFetchReservations={handleFetchReservations}
      />
    </div>
  );
}
