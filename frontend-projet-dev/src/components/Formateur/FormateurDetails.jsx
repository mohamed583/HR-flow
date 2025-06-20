/* eslint-disable no-unused-vars */
import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { getFormateurDetails, deleteFormateur } from "../../api/formateur";
import { getFormationsByFormateur } from "../../api/formation";
import { getMe } from "../../api/auth";
import { Box, Typography, Card, Button, Divider } from "@mui/material";
import { toast } from "react-toastify";
import { FiEdit, FiTrash2, FiUserCheck, FiArrowRight } from "react-icons/fi";
import AccessDeniedImage from "../../assets/accessDenied.png";
import "./FormateurDetails.css";

const pastel = {
  blue: "#A7C7E7",
  green: "#B2E2B8",
  pink: "#F6C6D4",
  yellow: "#FFF1B0",
  gray: "#E5E5E5",
  violet: "#D5C6E0",
};

const FormateurDetails = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [formateur, setFormateur] = useState(null);
  const [formations, setFormations] = useState([]);
  const [isAdmin, setIsAdmin] = useState(false);
  const [userRole, setUserRole] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const me = await getMe();
        setUserRole(me.roles);
        if (me.roles.includes("Admin")) {
          setIsAdmin(true);
          const fData = await getFormateurDetails(id);
          const formationData = await getFormationsByFormateur(id);
          setFormateur(fData);
          setFormations(formationData);
        }
      } catch (err) {
        toast.error("Erreur lors du chargement.");
      }
    };

    fetchData();
  }, [id]);

  const handleDelete = async () => {
    if (window.confirm("Voulez-vous vraiment supprimer ce formateur ?")) {
      try {
        await deleteFormateur(id);
        toast.success("Formateur supprimé.");
        navigate("/formateurs");
      } catch (err) {
        toast.error("Erreur de suppression.");
      }
    }
  };

  if (userRole === null) return <div>Chargement...</div>;
  if (!isAdmin)
    return (
      <Box className="access-denied-container">
        <img src={AccessDeniedImage} alt="Accès refusé" />
        <Typography variant="h5" className="access-denied-message">
          Accès refusé
        </Typography>
        <Typography className="access-denied-message">
          Vous n'avez pas les autorisations nécessaires.
        </Typography>
      </Box>
    );
  if (!formateur) return <div>Chargement des données...</div>;

  return (
    <Box className="formateur-detail-container">
      <Card className="formateur-card">
        <Typography variant="h5" className="formateur-title">
          {formateur.nom} {formateur.prenom}
        </Typography>
        <Typography>Email : {formateur.email}</Typography>
        <Typography>Domaine : {formateur.domaine}</Typography>
        <Typography>Salaire : {formateur.salaire} €</Typography>
        <Typography>Description : {formateur.description}</Typography>

        <Box className="formateur-actions">
          <Button
            className="action-button"
            onClick={() => navigate(`/formateur/${id}/change-login`)}
          >
            <FiUserCheck size={16} /> Login Infos
          </Button>
          <Button
            className="action-button edit"
            onClick={() => navigate(`/formateur/${id}/edit`)}
          >
            <FiEdit size={16} /> Modifier
          </Button>
          <Button className="action-button delete" onClick={handleDelete}>
            <FiTrash2 size={16} /> Supprimer
          </Button>
        </Box>
      </Card>

      <Typography variant="h6" className="formation-section-title">
        Formations associées
      </Typography>
      <Divider />
      <Box className="formation-list">
        {formations.length === 0 ? (
          <Typography>Aucune formation disponible.</Typography>
        ) : (
          formations.map((formation) => (
            <Card key={formation.id} className="formation-card">
              <Typography className="formation-title">
                {formation.titre}
              </Typography>
              <Typography>
                Début : {new Date(formation.dateDebut).toLocaleDateString()}
              </Typography>
              <Typography>
                Fin : {new Date(formation.dateFin).toLocaleDateString()}
              </Typography>
              <Button
                className="details-button"
                onClick={() => navigate(`/formation/${formation.id}`)}
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

export default FormateurDetails;
