import { apiClient } from "./apiClient"; // ton apiClient axios

export async function login({ email, password }) {
  const response = await apiClient.post("/auth/login", { email, password });
  return response.data.token;
}

export async function register({
  nom,
  prenom,
  email,
  password,
  confirmPassword,
}) {
  const response = await apiClient.post("/auth/register", {
    nom,
    prenom,
    email,
    password,
    confirmPassword,
  });
  return response.data.token;
}

export async function logout(refreshToken) {
  await apiClient.post("/auth/logout", { refreshToken });
}

export async function refreshToken(refreshToken) {
  const response = await apiClient.post("/auth/refresh", { refreshToken });
  return response.data;
}
// Fonction pour obtenir les données de l'utilisateur connecté
export async function getMe() {
  try {
    const response = await apiClient.get("/auth/me"); // Assurez-vous que l'endpoint est correct
    return response.data;
  } catch (error) {
    console.error(
      "Erreur lors de la récupération des données utilisateur :",
      error
    );
    throw error; // ou gérer l'erreur autrement
  }
}
export async function changeLoginInfo(id, loginInfo) {
  try {
    const response = await apiClient.put(`/auth/ChangeLoginInfo`, {
      id,
      data: loginInfo,
    });
    return response.data;
  } catch (error) {
    console.error(
      "Erreur lors du changement des identifiants de l'utilisateur :",
      error
    );
    throw error;
  }
}
