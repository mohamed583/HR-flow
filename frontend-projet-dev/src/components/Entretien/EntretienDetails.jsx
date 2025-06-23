// src/components/Entretien/EntretienDetails.jsx
import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { Box, Typography, Card, Button } from "@mui/material";
import { getMe } from "../../api/auth";
import { getEntretienDetails } from "../../api/entretien";
import AccessDeniedImage from "../../assets/accessDenied.png";
import "./EntretienDetails.css";

const pastelColors = {
  primary: "#A7C7E7",
  secondary: "#B2E2B8",
  accent: "#F6C6D4",
  yellow: "#FFF1B0",
  gray: "#E5E5E5",
  violet: "#D5C6E0",
  text: "#333",
};

const EntretienDetails = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [entretien, setEntretien] = useState(null);
  const [isAdmin, setIsAdmin] = useState(false);
  const [userRole, setUserRole] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const me = await getMe();
        setUserRole(me.roles);
        if (me.roles.includes("Admin")) {
          setIsAdmin(true);
          const data = await getEntretienDetails(id);
          setEntretien(data);
        }
      } catch (error) {
        console.error("Erreur lors du chargement des détails.", error);
      }
    };

    fetchData();
  }, [id]);

  if (userRole === null) return <div>Chargement...</div>;
  if (!isAdmin)
    return (
      <Box className="access-denied-container">
        <img src={AccessDeniedImage} alt="Accès refusé" />
        <Typography variant="h5" className="access-denied-message">
          Accès refusé
        </Typography>
        <Typography className="access-denied-message">
          Vous n'avez pas les autorisations nécessaires pour consulter cette
          page.
        </Typography>
      </Box>
    );

  if (!entretien) return <div>Chargement des données...</div>;

  return (
    <Box className="entretien-details-container">
      <Card className="entretien-card">
        <Typography variant="h5" className="entretien-title">
          Détails de l'entretien
        </Typography>
        <Typography>
          <strong>Date :</strong>{" "}
          {new Date(entretien.dateEntretien).toLocaleString()}
        </Typography>
        <Typography>
          <strong>Statut :</strong> {entretien.status}
        </Typography>
        <Typography>
          <strong>Commentaire :</strong> {entretien.commentaire || "Aucun"}
        </Typography>

        <Box className="section">
          <Typography variant="h6" className="section-title">
            Candidat associé
          </Typography>
          <Typography>
            {entretien.candidature.candidat.nom}{" "}
            {entretien.candidature.candidat.prenom}
          </Typography>
          <Button
            variant="contained"
            sx={{ backgroundColor: pastelColors.primary, mt: 1 }}
            onClick={() => navigate(`/candidature/${entretien.candidature.id}`)}
          >
            Voir la candidature
          </Button>
        </Box>

        <Box className="section">
          <Typography variant="h6" className="section-title">
            Employé référent
          </Typography>
          <Typography>
            {entretien.employe.nom} {entretien.employe.prenom} -{" "}
            {entretien.employe.metier}
          </Typography>
        </Box>
      </Card>
    </Box>
  );
};

export default EntretienDetails;
