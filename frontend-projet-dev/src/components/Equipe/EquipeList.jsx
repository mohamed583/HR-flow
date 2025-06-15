/* eslint-disable no-unused-vars */
import React, { useEffect, useState } from "react";
import { getEquipesByDepartement } from "../../api/equipe";
import { getDepartements } from "../../api/departement";
import { getMe } from "../../api/auth";
import { useNavigate } from "react-router-dom";
import { Box, Typography, Card, Button, Select, MenuItem } from "@mui/material";
import { FiArrowRight } from "react-icons/fi";
import { toast } from "react-toastify";
import AccessDeniedImage from "../../assets/accessDenied.png";
import "./EquipeList.css";

// Palette pastel
const pastelColors = {
  primary: "#A7C7E7",
  secondary: "#B2E2B8",
  accent: "#F6C6D4",
  background: "#F8F8F8",
  text: "#333",
};

const EquipeList = () => {
  const [departements, setDepartements] = useState([]);
  const [selectedDepartement, setSelectedDepartement] = useState(null);
  const [equipes, setEquipes] = useState([]);
  const [isAdmin, setIsAdmin] = useState(false);
  const [userRole, setUserRole] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchInitialData = async () => {
      try {
        const me = await getMe();
        setUserRole(me.roles);
        if (me.roles.includes("Admin")) {
          setIsAdmin(true);
        }

        const deps = await getDepartements();
        setDepartements(deps);

        if (deps.length > 0) {
          setSelectedDepartement(deps[0].id); // sélectionne le premier par défaut
          const equipesData = await getEquipesByDepartement(deps[0].id);
          setEquipes(equipesData);
        }
      } catch (error) {
        toast.error("Erreur lors du chargement.");
      }
    };

    fetchInitialData();
  }, []);

  const handleDepartementChange = async (e) => {
    const departementId = e.target.value;
    setSelectedDepartement(departementId);

    try {
      const equipesData = await getEquipesByDepartement(departementId);
      setEquipes(equipesData);
    } catch (error) {
      toast.error("Erreur lors de la récupération des équipes.");
    }
  };

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
          page.
        </Typography>
      </Box>
    );
  }

  return (
    <Box sx={{ p: 4 }}>
      <Typography variant="h4" sx={{ color: pastelColors.text, mb: 3 }}>
        Liste des Équipes
      </Typography>

      <Box sx={{ mb: 3, display: "flex", gap: 2, alignItems: "center" }}>
        <Typography variant="body1" sx={{ color: pastelColors.text }}>
          Choisir un département :
        </Typography>
        <Select
          value={selectedDepartement || ""}
          onChange={handleDepartementChange}
          size="small"
          sx={{
            backgroundColor: pastelColors.secondary,
            borderRadius: "10px",
            ".MuiSelect-select": { color: pastelColors.text },
          }}
        >
          {departements.map((dept) => (
            <MenuItem key={dept.id} value={dept.id}>
              {dept.nom}
            </MenuItem>
          ))}
        </Select>
      </Box>
      {/* Bouton pour créer une équipe */}
      <Button
        className="create-button"
        onClick={() => navigate("/equipe/create")}
      >
        Créer une Équipe
      </Button>

      <Box sx={{ display: "flex", flexWrap: "wrap", gap: 2 }}>
        {equipes.map((equipe) => (
          <Card key={equipe.id} className="equipe-card">
            <Typography className="equipe-title">{equipe.nom}</Typography>
            <Typography className="equipe-info">
              Employés : {equipe.employeIds.length}
            </Typography>
            <Button
              className="details-button"
              onClick={() => navigate(`/equipe/${equipe.id}`)}
            >
              Détails <FiArrowRight size={16} />
            </Button>
          </Card>
        ))}
      </Box>
    </Box>
  );
};

export default EquipeList;
