/* eslint-disable no-unused-vars */
import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { getMe } from "../../api/auth";
import { getEquipeDetails, deleteEquipe } from "../../api/equipe";
import { getEmployesByEquipe } from "../../api/employe";
import { Box, Typography, Card, Button, Divider } from "@mui/material";
import { FiEdit, FiTrash2, FiArrowLeft, FiArrowRight } from "react-icons/fi";
import { toast } from "react-toastify";
import AccessDeniedImage from "../../assets/accessDenied.png";
import "./EquipeDetails.css";

const pastelColors = {
  primary: "#A7C7E7",
  secondary: "#B2E2B8",
  accent: "#F6C6D4",
  background: "#F8F8F8",
  text: "#333",
  delete: "#F6C6D4",
};

const EquipeDetails = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [equipe, setEquipe] = useState(null);
  const [employes, setEmployes] = useState([]);
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
          setEquipe(data);

          const employeData = await getEmployesByEquipe(id);
          setEmployes(employeData);
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
        "Êtes-vous sûr de vouloir supprimer cette équipe ? Cette action est irréversible."
      )
    ) {
      try {
        await deleteEquipe(id);
        toast.success("Équipe supprimée avec succès !");
        navigate("/equipes");
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

  if (!equipe) {
    return <div>Chargement des données...</div>;
  }

  return (
    <Box className="equipe-detail-container">
      <Button onClick={() => navigate("/equipes")} className="back-button">
        <FiArrowLeft size={16} /> Retour
      </Button>

      <Card className="equipe-card">
        <Typography variant="h5" className="equipe-title">
          Équipe : {equipe.nom}
        </Typography>
        <Typography className="equipe-subtitle">
          Département : {equipe.departementNom}
        </Typography>

        <Box className="equipe-actions">
          <Button
            onClick={() => navigate(`/equipe/${id}/edit`)}
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
        Employés associés
      </Typography>
      <Divider />

      <Box className="employes-list">
        {employes.length === 0 ? (
          <Typography>Aucun employé associé.</Typography>
        ) : (
          employes.map((employe) => (
            <Card key={employe.id} className="employe-card">
              <Typography className="employe-title">
                {employe.prenom} {employe.nom}
              </Typography>
              <Typography>Métier : {employe.metier}</Typography>
              <Button
                className="details-button"
                onClick={() => navigate(`/employe/${employe.id}`)}
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

export default EquipeDetails;
