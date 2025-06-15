import { apiClient } from "./apiClient";
// Récupère la liste des formateurs
export const getFormateurs = async () => {
    try {
    const response = await apiClient.get("/formateur");
  return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des formateurs:", error);
    throw error;
  }
};