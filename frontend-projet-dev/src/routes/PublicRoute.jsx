import React, { useContext } from "react";
import { Navigate } from "react-router-dom";
import { AuthContext } from "../context/AuthContext";

export default function PublicRoute({ children }) {
  const { accessToken, loading } = useContext(AuthContext);

  if (loading) return <div>Chargement...</div>;

  return !accessToken ? children : <Navigate to="/" replace />;
}
