import { apiClient } from "./apiClient";

export const getFormationsByFormateur = async (id) => {
  try {
    const response = await apiClient.get(`/formation/formateur/${id}`);
    return response.data.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des  formations:", error);
    throw error;
  }
};
export const getFormations = async () => {
  try {
    const response = await apiClient.get(`/formation/`);
    return response.data.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des  formations:", error);
    throw error;
  }
};
export const createFormation = async (data) => {
  try {
    const response = await apiClient.post("/formation", data);
    return response.data.data; // Assuming response has { data: { ...formation } }
  } catch (error) {
    console.error("Erreur lors de la création de la formation:", error);
    throw error;
  }
};
export const getFormationById = async (id) => {
  try {
    const response = await apiClient.get(`/formation/${id}`);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération de la formation:", error);
    throw error;
  }
};

export const deleteFormation = async (id) => {
  try {
    const response = await apiClient.delete(`/formation/${id}`);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la suppression de la formation:", error);
    throw error;
  }
};
export const updateFormation = async (id, data) => {
  try {
    const response = apiClient.put(`/formation/${id}`, data);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la modification de la formation:", error);
    throw error;
  }
};
