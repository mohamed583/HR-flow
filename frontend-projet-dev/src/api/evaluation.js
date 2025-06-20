import { apiClient } from "./apiClient";

// Get my evaluations
export const getMesEvaluations = async () => {
  try {
    const res = await apiClient.get("/evaluation/mesEvaluations");
    return res.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des  evaluations:", error);
    throw error;
  }
};

// Get all evaluations
export const getAllEvaluations = async () => {
  try {
    const res = await apiClient.get("/evaluation");
    return res.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des  evaluations:", error);
    throw error;
  }
};

// Delete evaluation
export const deleteEvaluation = async (id) => {
  try {
    const res = await apiClient.delete(`/evaluation/${id}`);
    return res.data;
  } catch (error) {
    console.error("Erreur lors de la suppression de l'évaluation:", error);
    throw error;
  }
};

// Lancer une campagne
export const lancerCampagne = async () => {
  try {
    const res = await apiClient.post("/evaluation/lancerCampagne");
    return res.data;
  } catch (error) {
    console.error("Erreur lors de la création des évaluations:", error);
    throw error;
  }
};

export const createEvaluation = async (data) => {
  try {
    const res = await apiClient.post("/Evaluation", data);
    return res.data;
  } catch (error) {
    console.error("Erreur lors de la création de l'évaluation:", error);
    throw error;
  }
};
export const getEvaluationById = async (id) => {
  try {
    const res = await apiClient.get(`/evaluation/${id}`);
    return res.data;
  } catch (error) {
    console.error("Erreur lors de la récupération de l'évaluation:", error);
    throw error;
  }
};

export const approuverEvaluation = async (id, approuve) => {
  try {
    return await apiClient.put(
      `/evaluation/approuver/${id}?approuve=${approuve}`
    );
  } catch (error) {
    console.error("Erreur lors de la validation de l'évaluation:", error);
    throw error;
  }
};
export const finaliserParEmploye = async (id, data) => {
  try {
    return await apiClient.put(`/evaluation/finaliserParEmploye/${id}`, data);
  } catch (error) {
    console.error("Erreur lors de la finalisation de l'évaluation:", error);
    throw error;
  }
};

export const finaliserParManager = async (id, data) => {
  try {
    return await apiClient.put(`/evaluation/finaliserParManager/${id}`, data);
  } catch (error) {
    console.error("Erreur lors de la finalisation de l'évaluation:", error);
    throw error;
  }
};
