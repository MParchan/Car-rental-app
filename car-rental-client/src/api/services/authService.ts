import { axiosInstance, setAuthToken } from "../axios-config";
import { jwtDecode } from "jwt-decode";

export const register = async (credentials: {
    email: string;
    password: string;
    confirmPassword: string;
}) => {
    try {
        const response = await axiosInstance.post("/auth/register", credentials);
        return response.data;
    } catch (error) {
        console.error("Error registration:", error);
        throw error;
    }
};

export const login = async (credentials: { email: string; password: string }) => {
    try {
        const response = await axiosInstance.post("/auth/login", credentials);
        const token = response.data;
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
        const response = await axiosInstance.post("/auth/create-manager", credentials);
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

export const isAuthenticated = (): boolean => {
    const token = localStorage.getItem("accessToken");
    if (!token) {
        return false;
    }

    try {
        const decodedToken = jwtDecode(token);
        const currentTime = Date.now() / 1000;

        if (!decodedToken.exp || decodedToken.exp < currentTime) {
            localStorage.removeItem("accessToken");
            setAuthToken(undefined);
            return false;
        }

        return true;
    } catch (error) {
        console.error("Error decoding token:", error);
        return false;
    }
};

interface DecodedToken {
    [key: string]: string;
}

export const getUserRole = () => {
    const token = localStorage.getItem("accessToken");
    if (!token) {
        return null;
    }

    try {
        const decodedToken = jwtDecode<DecodedToken>(token);
        return decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    } catch (error) {
        console.error("Error decoding token:", error);
        return null;
    }
};
