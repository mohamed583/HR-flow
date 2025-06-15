/* eslint-disable no-unused-vars */
import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { getEquipeDetails, updateEquipe } from "../../api/equipe";
import { getDepartements } from "../../api/departement";
import { getMe } from "../../api/auth";
import { Box, Card, Typography, TextField, Button, MenuItem } from "@mui/material";
import { toast } from "react-toastify";
import "./EditEquipe.css";
import AccessDeniedImage from "../../assets/accessDenied.png";

const pastelColors = {
  primary: "#A7C7E7",
  secondary: "#B2E2B8",
  accent: "#F6C6D4",
  background: "#F8F8F8",
  text: "#333",
};

const EditEquipe = () => {
  const { id } = useParams();
  const navigate = useNavigate();

  const [equipe, setEquipe] = useState({ nom: "", departementId: "" });
  const [departements, setDepartements] = useState([]);
  const [isAdmin, setIsAdmin] = useState(false);
  const [userRole, setUserRole] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const me = await getMe();
        setUserRole(me.roles);
        if (me.roles.includes("Admin")) {
          setIsAdmin(true);
          const data = await getEquipeDetails(id);
          setEquipe({ nom: data.nom, departementId: data.departementId });
          const departementsData = await getDepartements();
          setDepartements(departementsData);
        }
      } catch (error) {
        toast.error("Erreur lors du chargement.");
      }
    };

    fetchData();
  }, [id]);

  const handleChange = (e) => {
    setEquipe({ ...equipe, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await updateEquipe(id, { id: parseInt(id), ...equipe });
      toast.success("Équipe mise à jour avec succès !");
      navigate(`/equipe/${id}`);
    } catch (error) {
      toast.error("Erreur lors de la mise à jour.");
    }
  };

  if (userRole === null) {
    return <div>Chargement...</div>;
  }

  if (!isAdmin) {
    return (
      <Box className="access-denied-container">
        <img src={AccessDeniedImage} alt="Accès refusé" />
        <Typography variant="h5" className="access-denied-message">
          Accès refusé
        </Typography>
        <Typography className="access-denied-message">
          Vous n'avez pas les autorisations nécessaires pour consulter cette page.
        </Typography>
      </Box>
    );
  }

  return (
    <Box className="edit-equipe-container">
      <Card className="edit-equipe-card">
        <Typography variant="h5" className="edit-equipe-title">
          Modifier l'équipe
        </Typography>
        <form onSubmit={handleSubmit} className="edit-equipe-form">
          <TextField
            label="Nom de l'équipe"
            name="nom"
            value={equipe.nom}
            onChange={handleChange}
            required
            fullWidth
            className="input-field"
          />

          <TextField
            select
            label="Département"
            name="departementId"
            value={equipe.departementId}
            onChange={handleChange}
            required
            fullWidth
            className="input-field"
          >
            {departements.map((dept) => (
              <MenuItem key={dept.id} value={dept.id}>
                {dept.nom}
              </MenuItem>
            ))}
          </TextField>

          <Box className="edit-equipe-actions">
            <Button
              type="submit"
              variant="contained"
              className="edit-button"
              style={{ backgroundColor: pastelColors.secondary }}
            >
              Enregistrer
            </Button>
            <Button
              onClick={() => navigate(`/equipe/${id}`)}
              variant="outlined"
              className="cancel-button"
            >
              Annuler
            </Button>
          </Box>
        </form>
      </Card>
    </Box>
  );
};

export default EditEquipe;
