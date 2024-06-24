import { axiosInstance, setAuthToken } from "../axios-config";

export const register = async (credentials: {
    email: string;
    password: string;
    confirmPassword: string;
}) => {
    try {
        const response = await axiosInstance.post("/register", credentials);
        return response.data;
    } catch (error) {
        console.error("Error registration:", error);
        throw error;
    }
};

export const login = async (credentials: { email: string; password: string }) => {
    try {
        const response = await axiosInstance.post("/login", credentials);
        const { token } = response.data;
        localStorage.setItem("accessToken", token);
        setAuthToken(token);
        return response.data;
    } catch (error) {
        console.error("Error logging in:", error);
        throw error;
    }
};

export const createManager = async (credentials: {
    email: string;
    password: string;
    confirmPassword: string;
}) => {
    try {
        const response = await axiosInstance.post("/create-manager", credentials);
        return response.data;
    } catch (error) {
        console.error("Error creating manager:", error);
        throw error;
    }
};

export const logout = () => {
    localStorage.removeItem("accessToken");
    setAuthToken(undefined);
};
