import axios from "axios";

// Récupère l'URL de l'API depuis les variables d'environnement
const API_URL = import.meta.env.VITE_API_URL || "http://localhost:5000";

// Crée l'instance axios
const apiClient = axios.create({
  baseURL: API_URL,
  headers: {
    "Content-Type": "application/json",
  },
});

// Ajoute un interceptor pour les requêtes sortantes
apiClient.interceptors.request.use((config) => {
  const token = localStorage.getItem("accessToken");
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

// Ajoute un interceptor pour les réponses
apiClient.interceptors.response.use(
  (response) => {
    // Si tout va bien, retourne la réponse normalement
    return response;
  },
  (error) => {
    // Si l'erreur est due à un accès non autorisé (401)
    if (error.response && error.response.status === 401) {
      console.warn("Non autorisé. Vous allez être redirigé.");
      // Vérifie si l'utilisateur est déjà sur la page de login
      if (window.location.pathname !== "/auth") {
        // Si l'utilisateur n'est pas sur la page de login, on le redirige vers la page de login
        localStorage.removeItem("accessToken");
        localStorage.removeItem("refreshToken");
        window.location.href = "/auth"; // Redirige vers la page de login
      }
    }
    return Promise.reject(error);
  }
);

export { apiClient };
export default API_URL;
