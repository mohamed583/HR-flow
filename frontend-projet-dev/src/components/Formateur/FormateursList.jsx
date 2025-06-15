/* eslint-disable no-unused-vars */
import React, { useEffect, useState } from "react";
import { getFormateurs } from "../../api/formateur";
import { getMe } from "../../api/auth";
import { useNavigate } from "react-router-dom";
import { Box, Card, Typography, Button } from "@mui/material";
import { FiArrowRight, FiPlus } from "react-icons/fi";
import { toast } from "react-toastify";
import "./FormateursList.css";

const pastelColors = {
  primary: "#A7C7E7",
  secondary: "#B2E2B8",
  accent: "#F6C6D4",
  background: "#F8F8F8",
  text: "#333",
};

const FormateurList = () => {
  const [formateurs, setFormateurs] = useState([]);
  const [isAdmin, setIsAdmin] = useState(false);
  const [userRole, setUserRole] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchData = async () => {
      try {
        const me = await getMe();
        setUserRole(me.roles);
        if (me.roles.includes("Admin")) {
          setIsAdmin(true);
          const data = await getFormateurs();
          setFormateurs(data);
        }
      } catch (error) {
        toast.error("Erreur lors du chargement des formateurs.");
      }
    };

    fetchData();
  }, []);

  if (userRole === null) return <div>Chargement...</div>;

  if (!isAdmin) {
    return (
      <Box className="access-denied-container">
        <Typography variant="h5" className="access-denied-message">
          Accès refusé
        </Typography>
        <Typography className="access-denied-message">
          Vous n'avez pas les autorisations nécessaires pour consulter cette
          page.
        </Typography>
      </Box>
    );
  }

  return (
    <Box className="formateur-list-container">
      <Box className="header">
        <Typography variant="h4" className="title">
          Liste des Formateurs
        </Typography>
        <Button
          className="create-button"
          onClick={() => navigate("/formateur/create")}
        >
          <FiPlus size={16} /> Créer un formateur
        </Button>
      </Box>

      <Box className="formateur-cards">
        {formateurs.map((formateur) => (
          <Card key={formateur.id} className="formateur-card">
            <Typography className="formateur-name">
              {formateur.prenom} {formateur.nom}
            </Typography>
            <Typography className="formateur-email">
              {formateur.email}
            </Typography>
            <Typography className="formateur-domaine">
              Domaine: {formateur.domaine}
            </Typography>
            <Button
              className="details-button"
              onClick={() => navigate(`/formateur/${formateur.id}`)}
            >
              Détails <FiArrowRight size={16} />
            </Button>
          </Card>
        ))}
      </Box>
    </Box>
  );
};

export default FormateurList;
