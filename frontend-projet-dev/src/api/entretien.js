import { apiClient } from "./apiClient";

export const getEntretiensByCandidature = async (candidatureId) => {
  try {
    const response = await apiClient.get(
      `/Entretien/Candidature/${candidatureId}`
    );
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des entretiens:", error);
    throw error;
  }
};
export const createEntretien = async (entretienData) => {
  try {
    const response = await apiClient.post("/Entretien", entretienData);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la création de l'entretien:", error);
    throw error;
  }
};

export const getEntretienDetails = async (id) => {
  try {
    const response = await apiClient.get(`/entretien/${id}`);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération de l'entretien:", error);
    throw error;
  }
};
export const getMyEntretiens = async () => {
  try {
    const response = await apiClient.get("/entretien/Employe/NonFinalise");
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des entretiens:", error);
    throw error;
  }
};
export const completeEntretien = (id, body) => {
  try {
    return apiClient.put(`/Entretien/Complete/${id}`, body);
  } catch (error) {
    console.error("Erreur lors de la modification de l'entretien:", error);
    throw error;
  }
};
export async function getEntretiens() {
  try {
    const response = await apiClient.get("/entretien");
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des entretiens:", error);
    throw error;
  }
}
