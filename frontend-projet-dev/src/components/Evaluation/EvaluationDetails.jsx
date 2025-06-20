/* COMPONENT FILE: components/Evaluation/EvaluationDetails.jsx */

import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Box, Card, Typography, Button, Divider } from "@mui/material";
import { toast } from "react-toastify";
import { getMe } from "../../api/auth";
import { getEvaluationById, approuverEvaluation } from "../../api/evaluation";
import AccessDeniedImage from "../../assets/accessDenied.png";
import "./EvaluationDetails.css";

const pastel = {
  blue: "#A7C7E7",
  green: "#B2E2B8",
  pink: "#F6C6D4",
  yellow: "#FFF1B0",
  gray: "#E5E5E5",
  violet: "#D5C6E0",
  text: "#333",
};

const EvaluationDetails = () => {
  const { id } = useParams();
  const [user, setUser] = useState(null);
  const [evaluation, setEvaluation] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      const me = await getMe();
      setUser(me);

      if (!me.roles.includes("Employe")) return;

      const res = await getEvaluationById(id);
      setEvaluation(res.data);
    };
    fetchData();
  }, [id]);

  const handleDecision = async (decision) => {
    try {
      await approuverEvaluation(id, decision);
      toast.success(decision ? "Évaluation approuvée." : "Évaluation rejetée.");
      setEvaluation((prev) => ({
        ...prev,
        estApprouve: decision ? "Approuvee" : "Rejetee",
      }));
    } catch {
      toast.error("Erreur lors de l'envoi de votre décision.");
    }
  };

  if (!user) return <div>Chargement...</div>;

  if (!user.roles.includes("Employe")) {
    return (
      <Box className="access-denied-container">
        <img src={AccessDeniedImage} alt="Accès refusé" />
        <Typography variant="h5" className="access-denied-message">
          Accès refusé
        </Typography>
        <Typography className="access-denied-message">
          Seuls les employés peuvent consulter cette évaluation.
        </Typography>
      </Box>
    );
  }

  if (!evaluation) return <div>Chargement des données...</div>;

  return (
    <Box className="evaluation-detail-container">
      <Card className="evaluation-card">
        <Typography variant="h5" className="evaluation-title">
          Détail de l'évaluation
        </Typography>
        <Divider sx={{ mb: 2 }} />

        <Typography>
          <strong>Date:</strong> {evaluation.dateEvaluation.slice(0, 10)}
        </Typography>
        <Typography>
          <strong>Description:</strong> {evaluation.description}
        </Typography>
        <Typography>
          <strong>Objectifs:</strong> {evaluation.objectifs}
        </Typography>
        <Typography>
          <strong>Note:</strong> {evaluation.note}
        </Typography>
        <Typography>
          <strong>Commentaires Employé:</strong>{" "}
          {evaluation.commentairesEmploye}
        </Typography>
        <Typography>
          <strong>Commentaires Responsable:</strong>{" "}
          {evaluation.commentairesResponsable}
        </Typography>
        <Typography>
          <strong>est-t-elle approuvée:</strong> {evaluation.estApprouve}
        </Typography>

        {evaluation.finaliseParEmploye &&
          evaluation.finaliseParManager &&
          evaluation.estApprouve === "EnCours" && (
            <Box className="decision-buttons">
              <Button
                onClick={() => handleDecision(true)}
                sx={{ backgroundColor: pastel.green, color: pastel.text }}
              >
                Approuver
              </Button>
              <Button
                onClick={() => handleDecision(false)}
                sx={{ backgroundColor: pastel.pink, color: pastel.text }}
              >
                Rejeter
              </Button>
            </Box>
          )}
      </Card>
    </Box>
  );
};

export default EvaluationDetails;
