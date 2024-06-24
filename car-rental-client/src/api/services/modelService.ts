import { Model } from "../../types/models/model.types";
import { axiosInstance } from "../axios-config";

export const getModels = async () => {
    try {
        const response = await axiosInstance.get(`/models`);
        return response.data;
    } catch (error) {
        console.error("Error fetching product:", error);
        throw error;
    }
};

export const getModel = async (modelId: number) => {
    try {
        const response = await axiosInstance.get(`/models/${modelId}`);
        return response.data;
    } catch (error) {
        console.error("Error fetching models:", error);
        throw error;
    }
};

export const createModel = async (model: Model) => {
    try {
        const response = await axiosInstance.post("/models", model);
        return response.data;
    } catch (error) {
        console.error("Error creating model:", error);
        throw error;
    }
};

export const updateModel = async (modelId: number, model: Model) => {
    try {
        const response = await axiosInstance.put(`/models/${modelId}`, model);
        return response.data;
    } catch (error) {
        console.error("Error updating model:", error);
        throw error;
    }
};

export const deleteModel = async (modelId: number) => {
    try {
        const response = await axiosInstance.delete(`/models/${modelId}`);
        return response.data;
    } catch (error) {
        console.error("Error deleting model:", error);
        throw error;
    }
};
