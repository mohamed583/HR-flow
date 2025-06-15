/* eslint-disable no-unused-vars */
import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import {
  getDepartementDetails,
  deleteDepartement,
} from "../../api/departement";
import { getMe } from "../../api/auth";
import { Box, Typography, Card, Button, Divider } from "@mui/material";
import { toast } from "react-toastify";
import AccessDeniedImage from "../../assets/accessDenied.png";
import { FiEdit, FiTrash2, FiArrowRight } from "react-icons/fi";
import "./DepartementDetails.css";

const pastelColors = {
  primary: "#A7C7E7",
  secondary: "#B2E2B8",
  accent: "#F6C6D4",
  background: "#F8F8F8",
  text: "#333",
  delete: "#F6C6D4",
};

const DepartementDetails = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [departement, setDepartement] = useState(null);
  const [isAdmin, setIsAdmin] = useState(false);
  const [userRole, setUserRole] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const me = await getMe();
        setUserRole(me.roles);
        if (me.roles.includes("Admin")) {
          setIsAdmin(true);
          const data = await getDepartementDetails(id);
          setDepartement(data);
        }
      } catch (error) {
        toast.error("Erreur lors du chargement des détails.");
      }
    };

    fetchData();
  }, [id]);

  const handleDelete = async () => {
    if (
      window.confirm(
        "Êtes-vous sûr de vouloir supprimer ce département ? Cette action est irréversible."
      )
    ) {
      try {
        await deleteDepartement(id);
        toast.success("Département supprimé avec succès !");
        navigate("/departements");
      } catch (error) {
        toast.error("Erreur lors de la suppression.");
      }
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
          Vous n'avez pas les autorisations nécessaires pour consulter cette
          page.
        </Typography>
      </Box>
    );
  }

  if (!departement) {
    return <div>Chargement des données...</div>;
  }

  return (
    <Box className="departement-detail-container">
      <Card className="departement-card">
        <Typography variant="h5" className="departement-title">
          Département : {departement.nom}
        </Typography>

        <Box className="departement-actions">
          <Button
            onClick={() => navigate(`/departement/${id}/edit`)}
            className="action-button edit"
          >
            <FiEdit size={16} /> Modifier
          </Button>
          <Button onClick={handleDelete} className="action-button delete">
            <FiTrash2 size={16} /> Supprimer
          </Button>
        </Box>
      </Card>

      <Typography
        variant="h6"
        sx={{ marginTop: 3, marginBottom: 1, color: pastelColors.text }}
      >
        Équipes associées
      </Typography>
      <Divider />

      <Box className="equipes-list">
        {departement.equipes.length === 0 ? (
          <Typography>Aucune équipe associée.</Typography>
        ) : (
          departement.equipes.map((equipe) => (
            <Card key={equipe.id} className="equipe-card">
              <Typography className="equipe-title">{equipe.nom}</Typography>
              <Typography>Employés : {equipe.employeIds.length}</Typography>
              <Button
                className="details-button"
                onClick={() => navigate(`/equipe/${equipe.id}`)}
              >
                Détails <FiArrowRight size={16} />
              </Button>
            </Card>
          ))
        )}
      </Box>
    </Box>
  );
};

export default DepartementDetails;
