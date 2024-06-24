import { Brand } from "../../types/models/brand.types";
import { axiosInstance } from "../axios-config";

export const getBrands = async () => {
    try {
        const response = await axiosInstance.get(`/brands`);
        return response.data;
    } catch (error) {
        console.error("Error fetching brands:", error);
        throw error;
    }
};

export const getBrand = async (brandId: number) => {
    try {
        const response = await axiosInstance.get(`/brands/${brandId}`);
        return response.data;
    } catch (error) {
        console.error("Error fetching brand:", error);
        throw error;
    }
};

export const createBrand = async (brand: Brand) => {
    try {
        const response = await axiosInstance.post("/brands", brand);
        return response.data;
    } catch (error) {
        console.error("Error creating brand:", error);
        throw error;
    }
};

export const updateBrand = async (brandId: number, brand: Brand) => {
    try {
        const response = await axiosInstance.put(`/brands/${brandId}`, brand);
        return response.data;
    } catch (error) {
        console.error("Error updating brand:", error);
        throw error;
    }
};

export const deleteBrand = async (brandId: number) => {
    try {
        const response = await axiosInstance.delete(`/brands/${brandId}`);
        return response.data;
    } catch (error) {
        console.error("Error deleting brand:", error);
        throw error;
    }
};
