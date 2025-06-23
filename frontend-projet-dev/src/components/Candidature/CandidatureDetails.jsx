/* eslint-disable no-unused-vars */
/* File: src/components/Candidature/CandidatureDetails.jsx */
import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { Box, Typography, Card, Button, Divider } from "@mui/material";
import { FiArrowRight } from "react-icons/fi";
import { getMe } from "../../api/auth";
import {
  getCandidatureDetails,
  updateCandidatureStatus,
} from "../../api/candidature";
import { getEntretiensByCandidature } from "../../api/entretien";
import { toast } from "react-toastify";
import API_URL from "../../api/apiClient";
import "./CandidatureDetails.css";

const pastel = {
  blue: "#A7C7E7",
  green: "#B2E2B8",
  pink: "#F6C6D4",
  yellow: "#FFF1B0",
  gray: "#E5E5E5",
  violet: "#D5C6E0",
};

const CandidatureDetails = () => {
  const { id } = useParams();
  const navigate = useNavigate();

  const [isAdmin, setIsAdmin] = useState(false);
  const [candidature, setCandidature] = useState(null);
  const [entretiens, setEntretiens] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const me = await getMe();
        if (me.roles.includes("Admin")) {
          setIsAdmin(true);
          const details = await getCandidatureDetails(id);
          setCandidature(details);
          const entretiensData = await getEntretiensByCandidature(id);
          setEntretiens(entretiensData);
        }
      } catch (err) {
        toast.error("Erreur de chargement des données");
      }
    };
    fetchData();
  }, [id]);

  const handleStatut = async (statut) => {
    try {
      await updateCandidatureStatus(id, statut);
      toast.success("Statut mis à jour");
      if (statut === 1) navigate(`/employe/create/${id}`);
    } catch (err) {
      toast.error("Erreur lors de la mise à jour du statut");
    }
  };

  if (!isAdmin) {
    return <Typography>Accès refusé</Typography>;
  }

  if (!candidature) return <Typography>Chargement...</Typography>;

  return (
    <Box className="candidature-detail-container">
      <Card className="candidature-card">
        <Typography variant="h5">
          Candidat : {candidature.candidat.nom} {candidature.candidat.prenom}
        </Typography>
        <Typography>Poste : {candidature.poste.nom}</Typography>
        <Typography>
          CV :{" "}
          <a
            href={`${API_URL}/${candidature.cvPath}`}
            target="_blank"
            rel="noopener noreferrer"
          >
            Voir le CV
          </a>
        </Typography>
        <Typography>Statut : {getStatusLabel(candidature.status)}</Typography>
        {candidature.status === 0 && (
          <Box className="action-buttons">
            <Button
              variant="contained"
              style={{ backgroundColor: pastel.green }}
              onClick={() => handleStatut(1)}
            >
              Accepter
            </Button>
            <Button
              variant="contained"
              style={{ backgroundColor: pastel.pink }}
              onClick={() => handleStatut(2)}
            >
              Refuser
            </Button>
          </Box>
        )}

        <Button
          variant="contained"
          sx={{ backgroundColor: pastel.blue, mt: 1 }}
          onClick={() => navigate(`/poste/${candidature.posteId}`)}
        >
          Voir le poste
        </Button>
      </Card>

      <Typography variant="h6" sx={{ mt: 4 }}>
        Entretiens
      </Typography>
      <Divider sx={{ mb: 2 }} />
      {candidature.status === 0 && (
        <Button
          variant="contained"
          style={{ backgroundColor: pastel.violet, marginBottom: "16px" }}
          onClick={() => navigate(`/entretien/${id}/create`)}
        >
          Créer entretien
        </Button>
      )}
      {entretiens.length === 0 ? (
        <Typography>Aucun entretien</Typography>
      ) : (
        entretiens.map((entretien) => (
          <Card key={entretien.id} className="entretien-card">
            <Typography>
              Date : {new Date(entretien.dateEntretien).toLocaleString()}
            </Typography>
            <Typography>Statut : {entretien.status}</Typography>
            <Button
              className="details-button"
              onClick={() => navigate(`/entretien/${entretien.id}`)}
            >
              Détails <FiArrowRight />
            </Button>
          </Card>
        ))
      )}
    </Box>
  );
};

const getStatusLabel = (status) => {
  switch (status) {
    case 0:
      return "En attente";
    case 1:
      return "Acceptée";
    case 2:
      return "Refusée";
    default:
      return "Inconnu";
  }
};

export default CandidatureDetails;
