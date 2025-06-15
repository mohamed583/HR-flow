import { apiClient } from "./apiClient";

export async function getDepartements() {
  try {
    const response = await apiClient.get("/department"); // Remplace "/departement" par l'endpoint correct
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des départements:", error);
    throw error;
  }
}
export const createDepartement = async (departementData) => {
  try {
    const response = await apiClient.post("/department", departementData);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la création du département:", error);
    throw error;
  }
};
export const getDepartementDetails = async (id) => {
  try {
    const response = await apiClient.get(`/department/${id}`);
    return response.data;
  } catch (error) {
    console.error(
      "Erreur lors de l'affichage des informations du département:",
      error
    );
    throw error;
  }
};

export const deleteDepartement = async (id) => {
  try {
    const response = await apiClient.delete(`/department/${id}`);
    return response;
  } catch (error) {
    console.error("Erreur lors de la suppression du département:", error);
    throw error;
  }
};
export const updateDepartement = async (id, data) => {
  try {
    const response = await apiClient.put(`/department/${id}`, data);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la modification du département:", error);
    throw error;
  }
};
