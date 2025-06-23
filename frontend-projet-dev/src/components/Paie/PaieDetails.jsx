/* eslint-disable no-unused-vars */
// File: src/components/Paie/PaieDetails.jsx
import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getMe } from "../../api/auth";
import { getPaieById } from "../../api/paie";
import { Box, Typography, Card, Button } from "@mui/material";
import { toast } from "react-toastify";
import AccessDeniedImage from "../../assets/accessDenied.png";
import "./PaieDetails.css";

const pastel = {
  primary: "#A7C7E7",
  secondary: "#B2E2B8",
  accent: "#F6C6D4",
  yellow: "#FFF1B0",
  gray: "#E5E5E5",
  violet: "#D5C6E0",
};

const PaieDetails = () => {
  const { id } = useParams();
  const [roles, setRoles] = useState([]);
  const [paie, setPaie] = useState(null);
  const [accessDenied, setAccessDenied] = useState(false);

  useEffect(() => {
    const loadData = async () => {
      try {
        const me = await getMe();
        setRoles(me.roles);
        if (!me.roles.includes("Employe")) {
          setAccessDenied(true);
          return;
        }

        const res = await getPaieById(id);
        setPaie(res.data);
      } catch (error) {
        toast.error("Erreur de chargement des données de la paie");
      }
    };

    loadData();
  }, [id]);

  if (accessDenied) {
    return (
      <Box className="access-denied-container">
        <img src={AccessDeniedImage} alt="Accès refusé" />
        <Typography variant="h5" className="access-denied-message">
          Accès refusé
        </Typography>
        <Typography className="access-denied-message">
          Cette page est réservée aux employés.
        </Typography>
      </Box>
    );
  }

  if (!paie) {
    return <div>Chargement...</div>;
  }

  return (
    <Box className="paie-detail-container">
      <Card className="paie-detail-card">
        <Typography variant="h5" className="paie-detail-title">
          Détails de la Paie
        </Typography>
        <Typography>Nom complet : {paie.nomComplet}</Typography>
        <Typography>Description : {paie.description}</Typography>
        <Typography>
          Date de paie : {new Date(paie.datePaie).toLocaleDateString()}
        </Typography>
        <Typography>Montant : {paie.montant} DT</Typography>
        <Typography>Avantages : {paie.avantages}</Typography>
        <Typography>Retenues : {paie.retenues}</Typography>

        <Button
          variant="contained"
          style={{ marginTop: 20, backgroundColor: pastel.secondary }}
          onClick={() => window.history.back()}
        >
          Retour
        </Button>
      </Card>
    </Box>
  );
};

export default PaieDetails;
