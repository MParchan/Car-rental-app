import React from "react";

const Layout = ({ children }: Readonly<{ children: React.ReactNode }>) => {
  return <div className="max-w-screen-xl mx-auto px-4 py-10">{children}</div>;
};

export default Layout;
