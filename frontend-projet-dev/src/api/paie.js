import { apiClient } from "./apiClient";

export const getMesPaies = async () => {
  try {
    const response = await apiClient.get("/paie/mes-paies");
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des paies:", error);
    throw error;
  }
};

export const getAllPaies = async () => {
  try {
    const response = await apiClient.get("/paie");
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des paies:", error);
    throw error;
  }
};

export const genererTousPaies = async () => {
  try {
    const response = await apiClient.post("/paie/effectuer-tous-employes");
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la création des paies:", error);
    throw error;
  }
};

export const getPaieById = async (id) => {
  try {
    const response = await apiClient.get(`/Paie/${id}`);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération du paie:", error);
    throw error;
  }
};

export const createPaie = async (data) => {
  try {
    const response = await apiClient.post("/Paie", data);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la création du paie:", error);
    throw error;
  }
};
