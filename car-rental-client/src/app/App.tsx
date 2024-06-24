import React from "react";
import { Route, Routes } from "react-router-dom";
import Navbar from "./components/navbar/Navbar";
import Layout from "./components/layout/Layout";
import MainPage from "./pages/MainPage";
import NotFoundPage from "./pages/NotFoundPage";
import CarModelPage from "./pages/CarModelPage";

function App() {
  return (
    <div>
      <Navbar />
      <Layout>
        <Routes>
          <Route path="/" element={<MainPage />} />
          <Route path="/m/:model/:id" element={<CarModelPage />} />
          <Route path="*" element={<NotFoundPage />} />
        </Routes>
      </Layout>
    </div>
  );
}

export default App;
