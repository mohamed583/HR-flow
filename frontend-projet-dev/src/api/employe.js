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