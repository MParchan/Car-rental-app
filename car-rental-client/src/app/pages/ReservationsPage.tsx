import React, { useEffect, useState } from "react";
import Loader from "../ui/loader/Loader";
import { Reservation } from "../../types/models/reservation.types";
import { getReservations } from "../../api/services/reservationService";
import UserReservationsTable from "../components/reservations/userReservationsTable/UserReservationsTable";
import { getUserRole } from "../../api/services/authService";
import AdminReservationsTable from "../components/reservations/adminReservationTable/AdminReservationsTable";
import Pagination from "../ui/pagination/Pagination";

export default function ReservationsPage() {
  const [reservations, setReservations] = useState<Reservation[]>([]);
  const [pageNumber, setPageNumber] = useState<number>(1);
  const [pageSize, setPageSize] = useState<number>(10);
  const [totalPages, setTotalPages] = useState<number>(1);
  const [sortField, setSortField] = useState<string>("ReservationId");
  const [sortOrder, setSortOrder] = useState<string>("desc");
  const [loading, setLoading] = useState(true);
  const role = getUserRole();

  useEffect(() => {
    const fetchReservations = async () => {
      try {
        const params = {
          pageNumber: pageNumber,
          pageSize: pageSize,
          sortField: sortField,
          sortOrder: sortOrder
        };
        const fetchedReservations = await getReservations(params);
        setReservations(fetchedReservations.data);
        setTotalPages(fetchedReservations.totalPages);
        setLoading(false);
      } catch (error) {
        setLoading(false);
      }
    };

    fetchReservations();
  }, [pageNumber, pageSize, sortField, sortOrder]);

  const handleFetchReservations = async () => {
    try {
      const params = {
        pageNumber: pageNumber,
        pageSize: pageSize,
        sortField: sortField,
        sortOrder: sortOrder
      };
      const fetchedReservations = await getReservations(params);
      setReservations(fetchedReservations.data);
      setTotalPages(fetchedReservations.totalPages);
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
          sortField={sortField}
          setSortOrder={setSortOrder}
          sortOrder={sortOrder}
          setSortField={setSortField}
        />
      ) : (
        <UserReservationsTable
          reservations={reservations}
          onFetchReservations={handleFetchReservations}
          sortField={sortField}
          setSortOrder={setSortOrder}
          sortOrder={sortOrder}
          setSortField={setSortField}
        />
      )}
      <Pagination
        pageNumber={pageNumber}
        totalPages={totalPages}
        pageSize={pageSize}
        setPageNumber={setPageNumber}
        setPageSize={setPageSize}
      />
    </div>
  );
}
