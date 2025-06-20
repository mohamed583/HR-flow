/* eslint-disable no-unused-vars */
// COMPONENT: EvaluationFinalisation.jsx
import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { getMe } from "../../api/auth";
import {
  getEvaluationById,
  finaliserParEmploye,
  finaliserParManager,
} from "../../api/evaluation";
import {
  Box,
  Typography,
  Card,
  Button,
  Divider,
  TextField,
} from "@mui/material";
import { toast } from "react-toastify";
import "./EvaluationFinalisation.css";

const colors = {
  primary: "#A7C7E7",
  success: "#B2E2B8",
  danger: "#F6C6D4",
  warning: "#FFF1B0",
  neutral: "#E5E5E5",
  accent: "#D5C6E0",
  text: "#333",
};

const EvaluationFinalisation = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [evaluation, setEvaluation] = useState(null);
  const [user, setUser] = useState(null);
  const [commentaireEmploye, setCommentaireEmploye] = useState("");
  const [commentaireResponsable, setCommentaireResponsable] = useState("");
  const [note, setNote] = useState(0);

  useEffect(() => {
    const fetchData = async () => {
      const me = await getMe();
      setUser(me);
      const res = await getEvaluationById(id);
      setEvaluation(res.data);
    };
    fetchData();
  }, [id]);

  const handleFinaliserEmploye = async () => {
    try {
      await finaliserParEmploye(id, {
        evaluationId: id,
        commentairesEmploye: commentaireEmploye,
      });
      toast.success("Évaluation finalisée par l'employé");
      navigate("/evaluations");
    } catch (e) {
      toast.error("Erreur lors de la finalisation.");
    }
  };

  const handleFinaliserManager = async () => {
    try {
      await finaliserParManager(id, {
        evaluationId: id,
        commentairesResponsable: commentaireResponsable,
        note: Number(note),
      });
      toast.success("Évaluation finalisée par le responsable");
      navigate("/evaluations");
    } catch (e) {
      toast.error("Erreur lors de la finalisation.");
    }
  };

  if (!user || !evaluation) return <div>Chargement...</div>;
  if (!user.roles.includes("Employe"))
    return <div>Accès refusé - réservé aux employés</div>;

  return (
    <Box className="finalisation-container">
      <Card className="finalisation-card">
        <Typography variant="h5" className="finalisation-title">
          Finaliser Évaluation
        </Typography>
        <Divider sx={{ mb: 2 }} />
        <Typography>Date: {evaluation.dateEvaluation.slice(0, 10)}</Typography>
        <Typography>Description: {evaluation.description}</Typography>
        <Typography>Objectifs: {evaluation.objectifs}</Typography>

        {!evaluation.finaliseParEmploye && !evaluation.finaliseParManager && (
          <>
            <TextField
              label="Commentaires Employé"
              fullWidth
              multiline
              rows={4}
              value={commentaireEmploye}
              onChange={(e) => setCommentaireEmploye(e.target.value)}
              sx={{ mt: 2 }}
            />
            <Button
              variant="contained"
              sx={{
                mt: 2,
                backgroundColor: colors.success,
                color: colors.text,
              }}
              onClick={handleFinaliserEmploye}
            >
              Finaliser
            </Button>
          </>
        )}

        {evaluation.finaliseParEmploye && !evaluation.finaliseParManager && (
          <>
            <Typography sx={{ mt: 2 }}>
              Commentaires Employé: {evaluation.commentairesEmploye}
            </Typography>
            <TextField
              label="Commentaires Responsable"
              fullWidth
              multiline
              rows={3}
              value={commentaireResponsable}
              onChange={(e) => setCommentaireResponsable(e.target.value)}
              sx={{ mt: 2 }}
            />
            <TextField
              label="Note (0-10)"
              type="number"
              fullWidth
              value={note}
              onChange={(e) => setNote(e.target.value)}
              sx={{ mt: 2 }}
            />
            <Button
              variant="contained"
              sx={{
                mt: 2,
                backgroundColor: colors.primary,
                color: colors.text,
              }}
              onClick={handleFinaliserManager}
            >
              Finaliser
            </Button>
          </>
        )}
      </Card>
    </Box>
  );
};

export default EvaluationFinalisation;
