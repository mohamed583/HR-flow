import { apiClient } from "./apiClient";

export async function getEquipesByDepartement(departementId) {
  try {
    const response = await apiClient.get(
      `/Equipe/Departement/${departementId}`
    );
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des équipes:", error);
    throw error;
  }
}
export const createEquipe = async (equipeData) => {
  try {
    const response = await apiClient.post("/Equipe", equipeData);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la création de l'équipe:", error);
    throw error;
  }
};

export const getEquipeDetails = async (id) => {
  try {
    const response = await apiClient.get(`/Equipe/${id}`);
  return response.data;
  } catch (error) {
    console.error("Erreur lors de la recption des informations de l'équipe:", error);
    throw error;
  }
};
export const deleteEquipe = async (id) => {
  try {
      await apiClient.delete(`/equipe/${id}`);
  } catch (error) {
    console.error("Erreur lors de la suppression de l'équipe:", error);
    throw error;
  }
};
export async function updateEquipe(id, data) {
  try {
    const response = await apiClient.put(`/equipe/${id}`, data);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la mise à jour de l'équipe:", error);
    throw error;
  }
}