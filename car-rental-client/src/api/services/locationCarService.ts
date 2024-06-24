import { LocationCar } from "../../types/models/locationCar.types";
import { axiosInstance } from "../axios-config";

export const getLocationCars = async () => {
    try {
        const response = await axiosInstance.get(`/location-cars`);
        return response.data;
    } catch (error) {
        console.error("Error fetching location cars:", error);
        throw error;
    }
};

export const getLocationCar = async (locationCarId: number) => {
    try {
        const response = await axiosInstance.get(`/location-cars/${locationCarId}`);
        return response.data;
    } catch (error) {
        console.error("Error fetching location car:", error);
        throw error;
    }
};

export const createLocationCar = async (locationCar: LocationCar) => {
    try {
        const response = await axiosInstance.post("/location-cars", locationCar);
        return response.data;
    } catch (error) {
        console.error("Error creating location car:", error);
        throw error;
    }
};

export const updateLocationCar = async (locationCarId: number, locationCar: LocationCar) => {
    try {
        const response = await axiosInstance.put(`/location-cars/${locationCarId}`, locationCar);
        return response.data;
    } catch (error) {
        console.error("Error updating location car:", error);
        throw error;
    }
};

export const deleteLocationCar = async (locationCarId: number) => {
    try {
        const response = await axiosInstance.delete(`/location-cars/${locationCarId}`);
        return response.data;
    } catch (error) {
        console.error("Error deleting location car:", error);
        throw error;
    }
};
