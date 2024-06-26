import React, { createContext, useEffect, useState } from "react";

interface AuthContextType {
  isLoggedIn: boolean;
  setLoggedIn: React.Dispatch<React.SetStateAction<boolean>>;
}

export const AuthContext = createContext<AuthContextType>({
  isLoggedIn: false,
  setLoggedIn: () => {}
});

export const AuthProvider = ({ children }: { children: React.ReactNode }) => {
  const [isLoggedIn, setLoggedIn] = useState<boolean>(() => {
    const storedState = localStorage.getItem("isLoggedIn");
    return storedState ? JSON.parse(storedState) : false;
  });

  useEffect(() => {
    localStorage.setItem("isLoggedIn", JSON.stringify(isLoggedIn));
  }, [isLoggedIn]);

  return (
    <AuthContext.Provider value={{ isLoggedIn, setLoggedIn }}>{children}</AuthContext.Provider>
  );
};
