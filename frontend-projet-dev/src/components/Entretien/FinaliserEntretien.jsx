/* eslint-disable no-unused-vars */
// Fichier : components/Entretien/FinaliserEntretien.jsx
import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { Box, Typography, Card, Button, TextField } from "@mui/material";
import { getMe } from "../../api/auth";
import { completeEntretien } from "../../api/entretien";
import { toast } from "react-toastify";
import AccessDeniedImage from "../../assets/accessDenied.png";
import "./FinaliserEntretien.css";

const colors = {
  primary: "#A7C7E7",
  success: "#B2E2B8",
  accent: "#F6C6D4",
  warning: "#FFF1B0",
  neutral: "#E5E5E5",
  violet: "#D5C6E0",
};

const FinaliserEntretien = () => {
  const { id } = useParams();
  const [role, setRole] = useState(null);
  const [commentaire, setCommentaire] = useState("");
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    const checkRole = async () => {
      const me = await getMe();
      setRole(me.roles);
    };
    checkRole();
  }, []);

  const handleSubmit = async () => {
    try {
      setLoading(true);
      await completeEntretien(id, { entretienId: id, model: { commentaire } });
      toast.success("Entretien finalisé avec succès.");
      navigate(-1);
    } catch (err) {
      toast.error("Erreur lors de la finalisation.");
    } finally {
      setLoading(false);
    }
  };

  if (role === null) return <div>Chargement...</div>;
  if (!role.includes("Employe")) {
    return (
      <Box className="access-denied-container">
        <img src={AccessDeniedImage} alt="Accès refusé" />
        <Typography variant="h5" className="access-denied-message">
          Accès refusé
        </Typography>
        <Typography className="access-denied-message">
          Vous n'avez pas les autorisations pour finaliser cet entretien.
        </Typography>
      </Box>
    );
  }

  return (
    <Box className="finaliser-entretien-container">
      <Card className="finaliser-card">
        <Typography variant="h5" className="titre">
          Finaliser l'entretien
        </Typography>

        <TextField
          label="Commentaire"
          multiline
          rows={4}
          value={commentaire}
          onChange={(e) => setCommentaire(e.target.value)}
          fullWidth
          sx={{ backgroundColor: colors.neutral, borderRadius: 1, mb: 2 }}
        />

        <Box className="button-group">
          <Button
            onClick={handleSubmit}
            variant="contained"
            disabled={loading}
            sx={{ backgroundColor: colors.success }}
          >
            {loading ? "Finalisation..." : "Finaliser"}
          </Button>
        </Box>
      </Card>
    </Box>
  );
};

export default FinaliserEntretien;
