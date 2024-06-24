import { CarType } from "../../types/models/carType.types";
import { axiosInstance } from "../axios-config";

export const getCarTypes = async () => {
    try {
        const response = await axiosInstance.get(`/car-types`);
        return response.data;
    } catch (error) {
        console.error("Error fetching car types:", error);
        throw error;
    }
};

export const getCarType = async (carTypeId: number) => {
    try {
        const response = await axiosInstance.get(`/car-types/${carTypeId}`);
        return response.data;
    } catch (error) {
        console.error("Error fetching car type:", error);
        throw error;
    }
};

export const createCarType = async (carType: CarType) => {
    try {
        const response = await axiosInstance.post("/car-types", carType);
        return response.data;
    } catch (error) {
        console.error("Error creating car type:", error);
        throw error;
    }
};

export const updateCarType = async (carTypeId: number, carType: CarType) => {
    try {
        const response = await axiosInstance.put(`/car-types/${carTypeId}`, carType);
        return response.data;
    } catch (error) {
        console.error("Error updating car type:", error);
        throw error;
    }
};

export const deleteCarType = async (carTypeId: number) => {
    try {
        const response = await axiosInstance.delete(`/car-types/${carTypeId}`);
        return response.data;
    } catch (error) {
        console.error("Error deleting car type:", error);
        throw error;
    }
};
