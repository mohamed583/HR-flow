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
export const createFormateur = async (formateur) => {
  try {
    const response = await apiClient.post("/formateur", { formateur });
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la création du formateur:", error);
    throw error;
  }
};
export const getFormateurDetails = async (id) => {
  try {
    const response = await apiClient.get(`/formateur/${id}`);
    return response.data;
  } catch (error) {
    console.error(
      "Erreur lors de la recuperation des informations du formateur:",
      error
    );
    throw error;
  }
};
export const deleteFormateur = async (id) => {
  try {
    return await apiClient.delete(`/formateur/${id}`);
  } catch (error) {
    console.error("Erreur lors de la suppression du formateur:", error);
    throw error;
  }
};
export const updateFormateur = async (id, formateur) => {
  try {
    return await apiClient.put(`/formateur/${id}`, { formateur });
  } catch (error) {
    console.error("Erreur lors de la modification du formateur:", error);
    throw error;
  }
};
