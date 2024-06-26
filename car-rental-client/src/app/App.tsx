import React from "react";
import Navbar from "./ui/navbar/Navbar";
import Layout from "./ui/layout/Layout";
import { AuthProvider } from "./contexts/authContext/AuthContext";
import AppRoutes from "./routes/AppRoutes";

function App() {
  return (
    <AuthProvider>
      <Navbar />
      <Layout>
        <AppRoutes />
      </Layout>
    </AuthProvider>
  );
}

export default App;
