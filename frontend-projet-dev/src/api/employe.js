import { apiClient } from "./apiClient";

export const getEmployesByEquipe = async (equipeId) => {
  try {
    const response = await apiClient.get(`/Employe/equipe/${equipeId}`);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des employés:", error);
    throw error;
  }
};
export const getEmployes = async () => {
  try {
    const response = await apiClient.get(`/Employe/`);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des employés:", error);
    throw error;
  }
};
export const getDetails = async (id) => {
  try {
    const response = await apiClient.get(`/employe/details/${id}`);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des employés:", error);
    throw error;
  }
};
export const getListeEquipe = async () => {
  try {
    const response = await apiClient.get("/employe/liste-equipe");
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des employés:", error);
    throw error;
  }
};
