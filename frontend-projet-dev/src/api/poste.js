import { apiClient } from "./apiClient"; // ton apiClient axios

// Fonction pour récupérer tous les postes
export const getPostes = async () => {
  try {
    const response = await apiClient.get("/poste");
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des postes", error);
    throw error;
  }
};
// Fonction pour récupérer un poste par son ID
export const getPosteById = async (id) => {
  try {
    const response = await apiClient.get(`/poste/${id}`); // Assurez-vous que l'URL correspond à votre API
    return response.data; // Retourne les données de la réponse
  } catch (error) {
    console.error("Erreur lors de la récupération du poste", error);
    throw error;
  }
};
// Modifier le statut du poste
export const updatePosteStatus = async (id, status) => {
  try {
    const response = await apiClient.patch(
      `/poste/statut/${id}?status=${status}`
    );
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la mise à jour du statut", error);
    throw error;
  }
};
// Supprimer un poste
export const deletePoste = async (id) => {
  try {
    const response = await apiClient.delete(`/poste/${id}`);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la suppression du poste", error);
    throw error;
  }
};
export const createPoste = async (posteData) => {
  try {
    const response = await apiClient.post("/poste", posteData);
    return response;
  } catch (error) {
    console.error("Erreur lors de la création du poste", error);
    throw error;
  }
};
// Fonction pour mettre à jour un poste
export async function updatePoste(id, posteData) {
  try {
    const response = await apiClient.put(`/poste/${id}`, posteData);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la mise à jour du poste :", error);
    throw error;
  }
}
export const applyForPoste = async (id, cvFile) => {
  const formData = new FormData();
  formData.append("cvFile", cvFile);

  try {
    const response = await apiClient.post(`/poste/${id}/apply`, formData, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la candidature:", error);
    throw error;
  }
};

export const getCandidaturesByPoste = async (posteId) => {
  try {
    const response = await apiClient.get(`/Poste/${posteId}/candidatures`);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des candidatures :", error);
    throw error;
  }
};
