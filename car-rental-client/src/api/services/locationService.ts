import { Location } from "../../types/models/location.types";
import { axiosInstance } from "../axios-config";

export const getLocations = async () => {
    try {
        const response = await axiosInstance.get(`/locations`);
        return response.data;
    } catch (error) {
        console.error("Error fetching locations:", error);
        throw error;
    }
};

export const getLocation = async (locationId: number) => {
    try {
        const response = await axiosInstance.get(`/locations/${locationId}`);
        return response.data;
    } catch (error) {
        console.error("Error fetching location:", error);
        throw error;
    }
};

export const createLocation = async (location: Location) => {
    try {
        const response = await axiosInstance.post("/locations", location);
        return response.data;
    } catch (error) {
        console.error("Error creating location:", error);
        throw error;
    }
};

export const updateLocation = async (locationId: number, location: Location) => {
    try {
        const response = await axiosInstance.put(`/locations/${locationId}`, location);
        return response.data;
    } catch (error) {
        console.error("Error updating location:", error);
        throw error;
    }
};

export const deleteLocation = async (locationId: number) => {
    try {
        const response = await axiosInstance.delete(`/locations/${locationId}`);
        return response.data;
    } catch (error) {
        console.error("Error deleting location:", error);
        throw error;
    }
};
