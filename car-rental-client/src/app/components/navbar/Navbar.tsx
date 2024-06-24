import React from "react";
import { Link } from "react-router-dom";

export default function Navbar() {
  return (
    <nav className="bg-zinc-100 border-gray-200 shadow-lg">
      <div className="max-w-screen-xl flex flex-wrap items-center justify-between mx-auto p-4">
        <Link to="/" className="flex items-center space-x-3 rtl:space-x-reverse">
          <img src="/assets/icons/logo.svg" className="h-8" alt="Car Rental Logo" />
          <span className="self-center text-2xl font-semibold whitespace-nowrap">Car Rental</span>
        </Link>
      </div>
    </nav>
  );
}
