import React, { createContext, useState, useEffect } from "react";
import * as authAPI from "../api/auth";

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [accessToken, setAccessToken] = useState(localStorage.getItem("accessToken"));
  const [refreshToken, setRefreshToken] = useState(localStorage.getItem("refreshToken"));
  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(true);

  // Charger les infos utilisateur au démarrage si token présent
  useEffect(() => {
    const fetchUser = async () => {
      if (!accessToken) {
        setLoading(false);
        return;
      }
      try {
        // Ici tu peux appeler l’API /auth/me pour récupérer les infos user (à ajouter côté backend)
        // Ex: const userData = await apiClient.get('/auth/me')
        // setUser(userData.data)
        setUser({ email: "user@example.com" }); // temporaire
      } catch {
        logout();
      }
      setLoading(false);
    };
    fetchUser();
  }, [accessToken]);

  const login = async ({ email, password }) => {
    const token = await authAPI.login({ email, password });
    localStorage.setItem("accessToken", token.accessToken);
    localStorage.setItem("refreshToken", token.refreshToken);
    setAccessToken(token.accessToken);
    setRefreshToken(token.refreshToken);
    setUser({ email }); // tu peux récupérer plus de données dans /auth/me
  };

  const register = async (data) => {
    const token = await authAPI.register(data);
    localStorage.setItem("accessToken", token.accessToken);
    localStorage.setItem("refreshToken", token.refreshToken);
    setAccessToken(token.accessToken);
    setRefreshToken(token.refreshToken);
    setUser({ email: data.email });
  };

  const logout = async () => {
    if (refreshToken) {
      await authAPI.logout(refreshToken);
    }
    localStorage.removeItem("accessToken");
    localStorage.removeItem("refreshToken");
    setAccessToken(null);
    setRefreshToken(null);
    setUser(null);
  };

  return (
    <AuthContext.Provider value={{ accessToken, refreshToken, user, login, register, logout, loading }}>
      {children}
    </AuthContext.Provider>
  );
};
