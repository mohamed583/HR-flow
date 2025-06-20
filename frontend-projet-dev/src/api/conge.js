import { apiClient } from "./apiClient";

export async function createConge(data) {
  try {
    const response = await apiClient.post("/conge", data);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la création du congé:", error);
    throw error;
  }
}

export const getMesConges = async () => {
  try {
    const res = await apiClient.get("/conge/mes-conges");
    return res.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des congés:", error);
    throw error;
  }
};

export const getAllConges = async () => {
  try {
    const res = await apiClient.get("/conge");
    return res.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des congés:", error);
    throw error;
  }
};

export const updateCongeStatus = async (id, status) => {
  try {
    const res = await apiClient.put(`/conge/status/${id}`, status);
    return res.data;
  } catch (error) {
    console.error("Erreur lors de la modification du status congé:", error);
    throw error;
  }
};

export const deleteConge = async (id) => {
  try {
    const res = await apiClient.delete(`/conge/${id}`);
    return res.data;
  } catch (error) {
    console.error("Erreur lors de la suppression du congé:", error);
    throw error;
  }
};
