import React, { useContext } from "react";
import { Link, useNavigate } from "react-router-dom";
import { logout } from "../../../api/services/authService";
import { AuthContext } from "../../contexts/authContext/AuthContext";

const Navbar = () => {
  const { isLoggedIn, setLoggedIn } = useContext(AuthContext);
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    setLoggedIn(false);
    navigate("/");
  };

  return (
    <nav className="bg-zinc-100 border-gray-200 shadow-lg">
      <div className="max-w-screen-xl flex flex-wrap items-center justify-between mx-auto p-4">
        <Link to="/" className="flex items-center space-x-1 sm:space-x-3 rtl:space-x-reverse">
          <img src="/assets/icons/logo.svg" className="h-8" alt="Car Rental Logo" />
          <span className="self-center text-2xl font-semibold whitespace-nowrap">Car Rental</span>
        </Link>
        {isLoggedIn ? (
          <div className="flex">
            <Link to="/reservations" className="sm:mx-2">
              <button className="w-24 sm:w-28 py-1 sm:py-2 rounded-lg border border-gray-800 text-gray-600 hover:bg-gray-200">
                Reservations
              </button>
            </Link>
            <button
              className="w-16 sm:w-20 py-1 sm:py-2 ml-2 sm:mx-2 rounded-lg border border-blue-400 bg-white text-blue-500 hover:text-white hover:bg-blue-400"
              onClick={handleLogout}
            >
              Log out
            </button>
          </div>
        ) : (
          <div className="flex">
            <Link to="/sign-in" className="sm:mx-2">
              <button className="w-16 sm:w-20 py-1 sm:py-2 rounded-lg border border-blue-400 bg-blue-500 text-white hover:bg-blue-400">
                Sign in
              </button>
            </Link>
            <Link to="/sign-up" className="ml-2 sm:mx-2">
              <button className="w-16 sm:w-20 py-1 sm:py-2 rounded-lg border border-blue-400 bg-white text-blue-500 hover:text-white hover:bg-blue-400">
                Sign up
              </button>
            </Link>
          </div>
        )}
      </div>
    </nav>
  );
};

export default Navbar;
