import React, { useContext, useEffect } from "react";
import { Navigate, Route, Routes } from "react-router-dom";
import MainPage from "../pages/MainPage";
import LoginPage from "../pages/LoginPage";
import RegisterPage from "../pages/RegisterPage";
import CarModelPage from "../pages/CarModelPage";
import NotFoundPage from "../pages/NotFoundPage";
import { AuthContext } from "../contexts/authContext/AuthContext";
import { isAuthenticated } from "../../api/services/authService";
import ReservationsPage from "../pages/ReservationsPage";

export default function AppRoutes() {
  const { isLoggedIn, setLoggedIn } = useContext(AuthContext);

  useEffect(() => {
    setLoggedIn(isAuthenticated());
  }, [setLoggedIn]);

  return (
    <Routes>
      <Route path="/" element={<MainPage />} />
      <Route path="/sign-in" element={<LoginPage />} />
      <Route path="/sign-up" element={<RegisterPage />} />
      <Route path="/m/:brandModel/:id" element={<CarModelPage />} />
      <Route
        path="/reservations"
        element={isLoggedIn ? <ReservationsPage /> : <Navigate to="/sign-in" />}
      />
      <Route path="*" element={<NotFoundPage />} />
    </Routes>
  );
}
