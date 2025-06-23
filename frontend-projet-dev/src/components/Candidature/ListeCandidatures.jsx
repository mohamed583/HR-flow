/* eslint-disable no-unused-vars */
/* File: src/components/Candidature/ListeCandidatures.jsx */
import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { getMe } from "../../api/auth";
import { getMesCandidatures, deleteCandidature } from "../../api/candidature";
import { Box, Typography, Card, Button, Grid } from "@mui/material";
import { FiArrowRight, FiTrash2 } from "react-icons/fi";
import { toast } from "react-toastify";
import AccessDeniedImage from "../../assets/accessDenied.png";
import "./ListeCandidatures.css";
import API_URL from "../../api/apiClient";

const pastel = {
  primary: "#A7C7E7",
  secondary: "#B2E2B8",
  accent: "#F6C6D4",
  yellow: "#FFF1B0",
  gray: "#E5E5E5",
  violet: "#D5C6E0",
};

const ListeCandidatures = () => {
  const [candidatures, setCandidatures] = useState([]);
  const [roleOk, setRoleOk] = useState(false);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchData = async () => {
      try {
        const me = await getMe();
        if (me.roles.includes("Candidat")) {
          setRoleOk(true);
          const res = await getMesCandidatures();
          if (Array.isArray(res)) setCandidatures(res);
        }
      } catch (err) {
        toast.error("Erreur de chargement des candidatures");
      } finally {
        setLoading(false);
      }
    };
    fetchData();
  }, []);

  const handleDelete = async (id) => {
    if (!window.confirm("Confirmer la suppression de cette candidature ?"))
      return;
    try {
      await deleteCandidature(id);
      toast.success("Candidature supprimée");
      setCandidatures((prev) => prev.filter((c) => c.id !== id));
    } catch (err) {
      toast.error("Erreur lors de la suppression");
    }
  };

  if (loading) return <div>Chargement...</div>;

  if (!roleOk) {
    return (
      <Box className="access-denied-container">
        <img src={AccessDeniedImage} alt="Accès refusé" />
        <Typography variant="h5" className="access-denied-message">
          Accès refusé
        </Typography>
        <Typography className="access-denied-message">
          Cette section est réservée aux candidats.
        </Typography>
      </Box>
    );
  }

  return (
    <Box className="candidature-container">
      <Typography variant="h5" className="candidature-title">
        Mes Candidatures
      </Typography>

      {candidatures.length === 0 ? (
        <Typography>Aucune candidature trouvée.</Typography>
      ) : (
        <Grid container spacing={2} className="candidature-grid">
          {candidatures.map((c) => (
            <Grid item xs={12} md={6} key={c.id}>
              <Card className="candidature-card">
                <Typography>
                  <strong>Poste :</strong> {c.poste.nom}
                </Typography>
                <Typography>
                  <strong>Description :</strong> {c.poste.description}
                </Typography>
                <Typography>
                  <strong>CV :</strong>{" "}
                  <a
                    href={`${API_URL}/${c.cvPath}`}
                    target="_blank"
                    rel="noopener noreferrer"
                  >
                    Afficher le CV
                  </a>
                </Typography>
                <Typography>
                  <strong>Status :</strong>{" "}
                  {c.status === 0
                    ? "En Cours"
                    : c.status === 1
                    ? "Approuvée"
                    : "Rejetée"}
                </Typography>

                <Box className="candidature-actions">
                  {c.status === 0 && (
                    <Button
                      variant="contained"
                      style={{ backgroundColor: pastel.accent }}
                      onClick={() => handleDelete(c.id)}
                    >
                      Supprimer <FiTrash2 size={16} />
                    </Button>
                  )}
                </Box>
              </Card>
            </Grid>
          ))}
        </Grid>
      )}
    </Box>
  );
};

export default ListeCandidatures;
