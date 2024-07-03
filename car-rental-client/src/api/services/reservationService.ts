import { Reservation } from "../../types/models/reservation.types";
import { axiosInstance } from "../axios-config";

export const getReservations = async (params = {}) => {
    try {
        const response = await axiosInstance.get(`/reservations`, { params });
        return response.data;
    } catch (error) {
        console.error("Error fetching reservations:", error);
        throw error;
    }
};

export const getReservation = async (reservationId: number) => {
    try {
        const response = await axiosInstance.get(`/reservations/${reservationId}`);
        return response.data;
    } catch (error) {
        console.error("Error fetching reservation:", error);
        throw error;
    }
};

export const createReservation = async (reservation: Reservation) => {
    try {
        const response = await axiosInstance.post("/reservations", reservation);
        return response.data;
    } catch (error) {
        console.error("Error creating reservation:", error);
        throw error;
    }
};

export const updateReservation = async (reservationId: number, reservation: Reservation) => {
    try {
        const response = await axiosInstance.put(`/reservations/${reservationId}`, reservation);
        return response.data;
    } catch (error) {
        console.error("Error updating reservation:", error);
        throw error;
    }
};

export const deleteReservation = async (reservationId: number) => {
    try {
        const response = await axiosInstance.delete(`/reservations/${reservationId}`);
        return response.data;
    } catch (error) {
        console.error("Error deleting reservation:", error);
        throw error;
    }
};

export const startReservation = async (reservationId: number) => {
    try {
        const response = await axiosInstance.patch(
            `/reservations/${reservationId}/start-resrvation`
        );
        return response.data;
    } catch (error) {
        console.error("Error starting reservation:", error);
        throw error;
    }
};

export const endReservation = async (reservationId: number) => {
    try {
        const response = await axiosInstance.patch(`/reservations/${reservationId}/end-resrvation`);
        return response.data;
    } catch (error) {
        console.error("Error ending reservation:", error);
        throw error;
    }
};

export const cancelReservation = async (reservationId: number) => {
    try {
        const response = await axiosInstance.patch(
            `/reservations/${reservationId}/cancel-resrvation`
        );
        return response.data;
    } catch (error) {
        console.error("Error canceling reservation:", error);
        throw error;
    }
};
