import React, { useEffect, useState } from "react";
import Loader from "../ui/loader/Loader";
import { Reservation } from "../../types/models/reservation.types";
import { getReservations } from "../../api/services/reservationService";
import UserReservationsTable from "../components/reservations/userReservationsTable/UserReservationsTable";
import { getUserRole } from "../../api/services/authService";
import AdminReservationsTable from "../components/reservations/adminReservationTable/AdminReservationsTable";

export default function ReservationsPage() {
  const [reservations, setReservations] = useState<Reservation[]>([]);
  const [loading, setLoading] = useState(true);
  const role = getUserRole();

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
      {role === "Admin" || role === "Manager" ? (
        <AdminReservationsTable
          reservations={reservations}
          onFetchReservations={handleFetchReservations}
        />
      ) : (
        <UserReservationsTable
          reservations={reservations}
          onFetchReservations={handleFetchReservations}
        />
      )}
    </div>
  );
}
