import axios from "axios";

export const axiosInstance = axios.create({
    baseURL: "https://localhost:7210/api",
    headers: {
        "Content-Type": "application/json"
    }
});

export const setAuthToken = (token?: string) => {
    if (token) {
        axiosInstance.defaults.headers.common["Authorization"] = `Bearer ${token}`;
    } else {
        delete axiosInstance.defaults.headers.common["Authorization"];
    }
};
