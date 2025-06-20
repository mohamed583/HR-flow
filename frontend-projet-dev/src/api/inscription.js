import { apiClient } from "./apiClient";

export const getInscriptionsByFormation = async (formationId) => {
  try {
    const response = await apiClient.get(
      `/inscription/formation/${formationId}`
    );
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des inscriptions:", error);
    throw error;
  }
};

export const getMesInscriptions = async () => {
  try {
    const response = await apiClient.get("/inscription/mes-inscriptions");
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des inscriptions:", error);
    throw error;
  }
};
export const getFormationsEtInscriptions = async () => {
  try {
    const response = await apiClient.get("/inscription/mes-formations");
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des inscriptions:", error);
    throw error;
  }
};
export const postulerFormation = async (id) => {
  try {
    return await apiClient.post(`/inscription/postuler/${id}`);
  } catch (error) {
    console.error("Erreur lors de l'inscription':", error);
    throw error;
  }
};

export const approveInscription = async (id) => {
  try {
    return await apiClient.post(`/inscription/${id}/approve`);
  } catch (error) {
    console.error(
      "Erreur lors du changement de status de l'inscription':",
      error
    );
    throw error;
  }
};

export const rejectInscription = async (id) => {
  try {
    return await apiClient.post(`/inscription/${id}/reject`);
  } catch (error) {
    console.error(
      "Erreur lors du changement de status de l'inscription':",
      error
    );
    throw error;
  }
};

export const deleteInscription = async (id) => {
  try {
    return await apiClient.delete(`/inscription/${id}`);
  } catch (error) {
    console.error("Erreur lors de la suppression de l'inscription':", error);
    throw error;
  }
};
