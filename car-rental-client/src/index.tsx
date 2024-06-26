import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import App from "./app/App";
import { BrowserRouter } from "react-router-dom";
import { axiosInstance } from "./api/axios-config";

axiosInstance.defaults.headers.common["Authorization"] =
  `Bearer ${localStorage.getItem("accessToken")}`;

const root = ReactDOM.createRoot(document.getElementById("root") as HTMLElement);
root.render(
  <React.StrictMode>
    <BrowserRouter>
      <App />
    </BrowserRouter>
  </React.StrictMode>
);
