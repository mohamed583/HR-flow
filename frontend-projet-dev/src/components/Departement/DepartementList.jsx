import React, { useEffect, useState } from "react";
import { getDepartements } from "../../api/departement";
import { Box, Typography, Card, Button } from "@mui/material";
import { getMe } from "../../api/auth";
import { toast } from "react-toastify";
import "./DepartementList.css";
import AccessDeniedImage from "../../assets/accessDenied.png";
import { useNavigate } from "react-router-dom";
import { FiArrowRight, FiPlus } from "react-icons/fi"; // Petite icône pour le bouton "Créer"

// Palette pastel
const pastelColors = {
  primary: "#A7C7E7",
  secondary: "#B2E2B8",
  accent: "#F6C6D4",
  background: "#F8F8F8",
  text: "#333",
};

const DepartementList = () => {
  const [departements, setDepartements] = useState([]);
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
        }

        const data = await getDepartements();
        setDepartements(data);
        // eslint-disable-next-line no-unused-vars
      } catch (error) {
        toast.error("Erreur lors du chargement des départements.");
      }
    };

    fetchData();
  }, []);

  if (userRole === null) {
    return <div>Chargement...</div>;
  }

  if (!isAdmin) {
    return (
      <Box className="access-denied-container">
        <img src={AccessDeniedImage} alt="Accès refusé" />
        <Typography
          variant="h5"
          className="access-denied-message"
          sx={{ mb: 2 }}
        >
          Accès refusé
        </Typography>
        <Typography className="access-denied-message">
          Vous n'avez pas les autorisations nécessaires pour consulter cette
          page. Merci de contacter un administrateur si vous pensez que c'est
          une erreur.
        </Typography>
      </Box>
    );
  }

  return (
    <Box sx={{ p: 4 }}>
      <Box
        sx={{
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
          mb: 3,
        }}
      >
        <Typography variant="h4" sx={{ color: pastelColors.text }}>
          Liste des Départements
        </Typography>

        <Button
          onClick={() => navigate("/departement/create")}
          sx={{
            backgroundColor: "#D5C6E0",
            color: "#fff",
            borderRadius: "20px",
            padding: "6px 14px",
            fontWeight: 500,
            display: "flex",
            alignItems: "center",
            gap: "6px",
            transition: "background-color 0.2s",
            "&:hover": {
              backgroundColor: "#E5E5E5",
            },
          }}
        >
          <FiPlus size={16} />
          Créer un Département
        </Button>
      </Box>

      <Box sx={{ display: "flex", flexWrap: "wrap", gap: 2 }}>
        {departements.map((dept) => (
          <Card key={dept.id} className="departement-card">
            <Typography className="departement-title">{dept.nom}</Typography>

            <Typography className="departement-info">
              Équipes associées : {dept.equipes.length}
            </Typography>

            <Button
              className="details-button"
              onClick={() => navigate(`/departement/${dept.id}`)}
            >
              Détails <FiArrowRight size={16} />
            </Button>
          </Card>
        ))}
      </Box>
    </Box>
  );
};

export default DepartementList;
