import { apiClient } from "./apiClient";

export const getMesCandidatures = async () => {
  try {
    const response = await apiClient.get("/candidature/mes-candidatures");
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des candidatures:", error);
    throw error;
  }
};

export const deleteCandidature = async (id) => {
  try {
    const response = await apiClient.delete(
      `/candidature/supprimer-candidature/${id}`
    );
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la suppression de la candidature:", error);
    throw error;
  }
};
export const getCandidatureDetails = async (id) => {
  try {
    const response = await apiClient.get(`/Candidature/details/${id}`);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération de la candidature:", error);
    throw error;
  }
};

export const updateCandidatureStatus = async (id, status) => {
  try {
    return await apiClient.post(`/Candidature/modifier-statut/${id}`, status);
  } catch (error) {
    console.error(
      "Erreur lors de la modification du statut de la candidature:",
      error
    );
    throw error;
  }
};
export const downloadCv = async (candidatureId) => {
  try {
    return await apiClient.get(`/candidature/download-cv/${candidatureId}`, {
      responseType: "blob",
    });
  } catch (error) {
    console.error("Erreur lors du téléchargement du CV:", error);
    throw error;
  }
};
export const transformCandidatEnEmploye = async (data) => {
  try {
    const response = await apiClient.post(
      "candidature/transformation-employe",
      data
    );
    return response.data;
  } catch (error) {
    console.error("Erreur lors de création de l'employe:", error);
    throw error;
  }
};
export const getCvMatchByPosteId = async (posteId) => {
  try {
    const response = await apiClient.get(`/match/${posteId}`);
    return response.data;
  } catch (err) {
    console.error("Erreur lors du fetch des scores AI:", err);
    throw err;
  }
};
