import React from "react";

export default function Layout({ children }: Readonly<{ children: React.ReactNode }>) {
  return <div className="max-w-screen-xl mx-auto px-4 pt-10">{children}</div>;
}
