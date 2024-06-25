import { Car } from "../../types/models/car.types";
import { axiosInstance } from "../axios-config";

export const getCars = async () => {
    try {
        const response = await axiosInstance.get(`/cars`);
        return response.data;
    } catch (error) {
        console.error("Error fetching cars:", error);
        throw error;
    }
};

export const getAvailableCars = async (
    modelId: number,
    locationId: number,
    startDate: string,
    endDate: string
) => {
    try {
        const response = await axiosInstance.get(
            `/cars/available?modelId=${modelId}&locationId=${locationId}&startDate=${startDate}&endDate=${endDate}`
        );
        return response.data;
    } catch (error) {
        console.error("Error fetching available cars:", error);
        throw error;
    }
};

export const getCar = async (carId: number) => {
    try {
        const response = await axiosInstance.get(`/cars/${carId}`);
        return response.data;
    } catch (error) {
        console.error("Error fetching car:", error);
        throw error;
    }
};

export const createCar = async (car: Car) => {
    try {
        const response = await axiosInstance.post("/cars", car);
        return response.data;
    } catch (error) {
        console.error("Error creating car:", error);
        throw error;
    }
};

export const updateCar = async (carId: number, car: Car) => {
    try {
        const response = await axiosInstance.put(`/cars/${carId}`, car);
        return response.data;
    } catch (error) {
        console.error("Error updating car:", error);
        throw error;
    }
};

export const deleteCar = async (carId: number) => {
    try {
        const response = await axiosInstance.delete(`/cars/${carId}`);
        return response.data;
    } catch (error) {
        console.error("Error deleting car:", error);
        throw error;
    }
};
